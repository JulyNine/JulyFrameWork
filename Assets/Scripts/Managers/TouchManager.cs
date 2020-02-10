using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRubyShared;
public class TouchManager : MonoBehaviour
{
    public TapGestureRecognizer tapGesture;
    public TapGestureRecognizer doubleTapGesture;
    public TapGestureRecognizer tripleTapGesture;
    public SwipeGestureRecognizer swipeGesture;
    public PanGestureRecognizer panGesture;
    public ScaleGestureRecognizer scaleGesture;
    public RotateGestureRecognizer rotateGesture;
    public LongPressGestureRecognizer longPressGesture;
    // Start is called before the first frame update
    private static TouchManager s_Instance;
    // Note: this is set on Awake()
    public static TouchManager Instance
    {
        get
        {
            return s_Instance;
        }
    }
    void Awake()
    {
        s_Instance = this;
    }


    void Start()
    {
        CreateDoubleTapGesture();
        CreateTapGesture();
        CreateSwipeGesture();
        CreatePanGesture();
        CreateScaleGesture();
        CreateRotateGesture();
        CreateLongPressGesture();

        // pan, scale and rotate can all happen simultaneously
        panGesture.AllowSimultaneousExecution(scaleGesture);
        panGesture.AllowSimultaneousExecution(rotateGesture);
        scaleGesture.AllowSimultaneousExecution(rotateGesture);

        // prevent the one special no-pass button from passing through,
        //  even though the parent scroll view allows pass through (see FingerScript.PassThroughObjects)
        FingersScript.Instance.CaptureGestureHandler = CaptureGestureHandler;

        // show touches, only do this for debugging as it can interfere with other canvases
        FingersScript.Instance.ShowTouches = true;
    }
    private void TapGestureCallback(DigitalRubyShared.GestureRecognizer gesture)
    {
      //  Debug.LogFormat("TouchManager Tap state: {0}", gesture.State);
        if (gesture.State == GestureRecognizerState.Ended)
        {

        }
    }

    private void CreateTapGesture()
    {
        tapGesture = new TapGestureRecognizer();
        tapGesture.StateUpdated += TapGestureCallback;
        tapGesture.RequireGestureRecognizerToFail = doubleTapGesture;
        FingersScript.Instance.AddGesture(tapGesture);
    }

    private void DoubleTapGestureCallback(DigitalRubyShared.GestureRecognizer gesture)
    {
     //   Debug.LogFormat("TouchManager DoubleTap state: {0}", gesture.State);
        if (gesture.State == GestureRecognizerState.Ended)
        {

        }
    }

    private void CreateDoubleTapGesture()
    {
        doubleTapGesture = new TapGestureRecognizer();
        doubleTapGesture.NumberOfTapsRequired = 2;
        doubleTapGesture.StateUpdated += DoubleTapGestureCallback;
        doubleTapGesture.RequireGestureRecognizerToFail = tripleTapGesture;
        FingersScript.Instance.AddGesture(doubleTapGesture);
    }

    private void SwipeGestureCallback(DigitalRubyShared.GestureRecognizer gesture)
    {
       // Debug.LogFormat("TouchManager Swipe state: {0}", gesture.State);
        if (gesture.State == GestureRecognizerState.Ended)
        {
          
        }
    }

    private void CreateSwipeGesture()
    {
        swipeGesture = new SwipeGestureRecognizer();
        swipeGesture.Direction = SwipeGestureRecognizerDirection.Any;
        swipeGesture.EndMode = SwipeGestureRecognizerEndMode.EndWhenTouchEnds;
       // swipeGesture.EndMode = SwipeGestureRecognizerEndMode.EndContinusously;
        swipeGesture.StateUpdated += SwipeGestureCallback;
        swipeGesture.MinimumSpeedUnits = 0;
        swipeGesture.DirectionThreshold = 2.5f; // allow a swipe, regardless of slope
        FingersScript.Instance.AddGesture(swipeGesture);
    }


    private void PanGestureCallback(DigitalRubyShared.GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Executing)
        {
     
        }
    }

    private void CreatePanGesture()
    {
        panGesture = new PanGestureRecognizer();
        panGesture.MinimumNumberOfTouchesToTrack = 2;
        panGesture.StateUpdated += PanGestureCallback;
        FingersScript.Instance.AddGesture(panGesture);
    }

    private void ScaleGestureCallback(DigitalRubyShared.GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Executing)
        {
           
        }
    }

    private void CreateScaleGesture()
    {
        scaleGesture = new ScaleGestureRecognizer();
        scaleGesture.StateUpdated += ScaleGestureCallback;
        FingersScript.Instance.AddGesture(scaleGesture);
    }

    private void RotateGestureCallback(DigitalRubyShared.GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Executing)
        {

        }
    }

    private void CreateRotateGesture()
    {
        rotateGesture = new RotateGestureRecognizer();
        rotateGesture.StateUpdated += RotateGestureCallback;
        FingersScript.Instance.AddGesture(rotateGesture);
    }

    private void LongPressGestureCallback(DigitalRubyShared.GestureRecognizer gesture)
    {
      //  Debug.LogFormat("TouchManager LongPress state: {0}", gesture.State);
        if (gesture.State == GestureRecognizerState.Began)
        {

        }
        else if (gesture.State == GestureRecognizerState.Executing)
        {

        }
        else if (gesture.State == GestureRecognizerState.Ended)
        {
          
        }
    }

    private void CreateLongPressGesture()
    {
        longPressGesture = new LongPressGestureRecognizer();
        longPressGesture.MaximumNumberOfTouchesToTrack = 1;
        longPressGesture.StateUpdated += LongPressGestureCallback;
        FingersScript.Instance.AddGesture(longPressGesture);
    }

    private void PlatformSpecificViewTapUpdated(DigitalRubyShared.GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Ended)
        {
            Debug.Log("You triple tapped the platform specific label!");
        }
    }

    private void CreatePlatformSpecificViewTripleTapGesture()
    {
        tripleTapGesture = new TapGestureRecognizer();
        tripleTapGesture.StateUpdated += PlatformSpecificViewTapUpdated;
        tripleTapGesture.NumberOfTapsRequired = 3;
       // tripleTapGesture.PlatformSpecificView = bottomLabel.gameObject;
        FingersScript.Instance.AddGesture(tripleTapGesture);
    }

    private static bool? CaptureGestureHandler(GameObject obj)
    {
        // I've named objects PassThrough* if the gesture should pass through and NoPass* if the gesture should be gobbled up, everything else gets default behavior
        if (obj.name.StartsWith("PassThrough"))
        {
            // allow the pass through for any element named "PassThrough*"
            return false;
        }
        else if (obj.name.StartsWith("NoPass"))
        {
            // prevent the gesture from passing through, this is done on some of the buttons and the bottom text so that only
            // the triple tap gesture can tap on it
            return true;
        }

        // fall-back to default behavior for anything else
        return null;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
