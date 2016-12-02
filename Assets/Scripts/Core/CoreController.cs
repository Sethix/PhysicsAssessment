using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class CoreController : NetworkBehaviour
{

    #region Variables

    #region Input

    private   Vector2 _movementInput;
    protected Vector2 movementInput
    { get { return _movementInput; } }

    private   bool _actionOne;
    private   bool _actionOnePrevFrame;
    protected bool actionOne
    { get { return _actionOne; } }
    protected bool actionOnePressed
    { get { return _actionOne && !_actionOnePrevFrame; } }
    protected bool actionOneUnpressed
    { get { return !_actionOne && _actionOnePrevFrame; } }

    private   bool _actionTwo;
    private   bool _actionTwoPrevFrame;
    protected bool actionTwo
    { get { return _actionTwo; } }
    protected bool actionTwoPressed
    { get { return _actionTwo && !_actionTwoPrevFrame; } }
    protected bool actionTwoUnpressed
    { get { return !_actionTwo && _actionTwoPrevFrame; } }

    private   bool _actionThree;
    private   bool _actionThreePrevFrame;
    protected bool actionThree
    { get { return _actionThree; } }
    protected bool actionThreePressed
    { get { return _actionThree && !_actionThreePrevFrame; } }
    protected bool actionThreeUnpressed
    { get { return !_actionThree && _actionThreePrevFrame; } }

    #endregion

    #endregion

    #region Functions

    #region UnityFunctions

    protected virtual void Start()
    { }

    // Update is called once per frame
    protected virtual void Update ()
    {
        UpdatePreviousFrame();
        SetInput();
    }

    #endregion

    #region Virtual

    protected virtual void SetInput()
    {

    }

    #endregion

    #region Input

    protected void SetMovementInput(Vector2 mVec)
    {
        _movementInput = mVec;
    }

    protected void SetActionOne(bool action)
    {
        _actionOne = action;
    }

    protected void SetActionTwo(bool action)
    {
        _actionTwo = action;
    }

    protected void SetActionThree(bool action)
    {
        _actionThree = action;
    }

    void UpdatePreviousFrame()
    {
        _actionOnePrevFrame   = _actionOne;
        _actionTwoPrevFrame   = _actionTwo;
        _actionThreePrevFrame = _actionThree;
    }

    #endregion

    #endregion

}
