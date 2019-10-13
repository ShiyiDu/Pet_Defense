using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class CondTrigger : StateMachineBehaviour
{
    //public bool a, b, c, d, e, f;

    //the trigger in animator
    public string trigger;
    [TextArea(3, 20)]
    public string expression; // currently we have ||, &&, (), and you have to use space to seperate them

    private string[] exp;
    private UnitBehaviour unit;
    private bool triggerSet = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger(trigger);
        unit = animator.GetComponent<UnitBehaviour>();
        exp = MakeExpReadable();
        triggerSet = false;
        //Debug.Log("expression form:" + string.Join(" ", exp));
        //Debug.Log("parsing result: " + ParseBool(0, exp.Length));

        //cond trigger shouldn't be triggered immediately, it should wait until everyone finished checking 

        //if (ParseBool(0, exp.Length)) {
        //    Debug.Log("Set Trigger: " + trigger + ", On Cond: " + string.Join(" ", exp));
        //    animator.ResetTrigger(trigger);
        //    animator.SetTrigger(trigger);
        //    triggerSet = true;
        //}
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (triggerSet) return;
        if (ParseBool(0, exp.Length)) {
            Debug.Log("Set Trigger: " + trigger + ", On Cond: " + string.Join(" ", exp));
            animator.ResetTrigger(trigger);
            animator.SetTrigger(trigger);
            triggerSet = true;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    private string[] MakeExpReadable()
    {
        List<string> result = new List<string>();
        StringBuilder stringBuilder = new StringBuilder();
        void AddString()
        {
            if (stringBuilder.Length > 0) result.Add(stringBuilder.ToString());
            stringBuilder.Clear();
        }
        for (int i = 0; i < expression.Length; i++) {
            if (expression[i] == ' ') {
                AddString();
                continue;
            } else if (expression[i] == '|') {
                AddString();
                if (expression[i + 1] == '|') {
                    stringBuilder.Append("||");
                    i++;
                } else {
                    Debug.LogError("parsing error: " + string.Join(" ", exp));
                }
                AddString();
            } else if (expression[i] == '&') {
                AddString();
                if (expression[i + 1] == '&') {
                    stringBuilder.Append("&&");
                    i++;
                } else {
                    Debug.LogError("parsing error " + string.Join(" ", exp));
                }
                AddString();
            } else if (expression[i] == '!') {
                AddString();
                stringBuilder.Append('!');
                if (expression[i + 1] == '(') {
                    stringBuilder.Append("(");
                    i++;
                    AddString();
                }

            } else if (expression[i] == '(') {
                AddString();
                stringBuilder.Append('(');
                AddString();
            } else if (expression[i] == ')') {
                AddString();
                stringBuilder.Append(')');
                AddString();
            } else {
                stringBuilder.Append(expression[i]);
            }
        }
        AddString();
        return result.ToArray();
    }

    //include start, exclude end
    private bool ParseBool(int start, int end)
    {
        if (end == start) {
            Debug.LogWarning("Empty expression, return true");
            return true;
        }
        //base case
        if (end - start <= 1) {
            if (exp[start][0] == '!') return !GetValue(exp[start].Remove(0, 1));
            return GetValue(exp[start]);
        }

        //change form !(a && b) == !a || !b, !(a || b) == !a && !b

        //if meet bracket skip unless start and end are both bracket
        //count the number of left bracket met
        //first find or, skip when left bracket > 0
        for (int i = start; i < end; i++) {
            switch (exp[i]) {
                case "||":
                    return ParseBool(start, i) || ParseBool(i + 1, end);
                    break;
                case "(":
                    i = GetRightBracket(i);
                    break;
                case "!(":
                    i = ChangeNegForm(i);
                    break;
                default:
                    continue;
            }
        }
        //second find and

        for (int i = start; i < end; i++) {
            switch (exp[i]) {
                case "&&":
                    return ParseBool(start, i) && ParseBool(i + 1, end);
                    break;
                case "(":
                    i = GetRightBracket(i);
                    break;
                case "!(":
                    i = ChangeNegForm(i);
                    Debug.LogError("parsing error: shouldn't be able to encounter !( anymore " + string.Join(" ", exp));
                    break;
                default:
                    continue;
            }
        }

        if (exp[start] == "(" && exp[end - 1] == ")") return ParseBool(start + 1, end - 1);

        Debug.LogError("parsing error " + string.Join(" ", exp));
        return false;
    }

    private int GetRightBracket(int start)
    {
        for (int i = start + 1; i < exp.Length; i++) {
            switch (exp[i]) {
                case "(":
                    i = GetRightBracket(i);
                    break;
                case "!(":
                    i = ChangeNegForm(i);
                    break;
                case ")":
                    return i;
                    break;
                default:
                    break;
            }
        }
        Debug.LogError("parsing bracket error! " + string.Join(" ", exp));
        return -1;
    }

    private int ChangeNegForm(int start)
    {
        exp[start] = "(";
        //start include (, you stop when you encounter ), call anoather change form when you encounter (
        for (int i = start + 1; i < exp.Length; i++) {
            switch (exp[i]) {
                case "||":
                    exp[i] = "&&";
                    break;
                case "&&":
                    exp[i] = "||";
                    break;
                case "!(":
                    i = ChangeNegForm(i);
                    break;
                case "(":
                    i = GetRightBracket(i);
                    break;
                case ")":
                    return i;
                    break;
                default:
                    exp[i] = "!" + exp[i];
                    break;
            }
        }
        Debug.LogError("parsing neg bracket error! " + string.Join(" ", exp));
        return -1;
    }

    private bool GetValue(string boolName)
    {
        return (bool)unit.GetType().GetField(boolName).GetValue(unit);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
