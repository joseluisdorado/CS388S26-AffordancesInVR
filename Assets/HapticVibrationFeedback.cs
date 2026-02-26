using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HapticVibrationFeedback : MonoBehaviour
{
    // intensity or amplitude of intended vibration
    float intensity;

    // This setters is needed in order to change the propoerty value from the affordance receiver
    public void SetIntensity(float intensity)
    {
        this.intensity = intensity;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // We check if intensity has been increased based on the state in the affordance receiver and its corresponding theme
        if (intensity > 0)
        {
            // Get the interactable component of the current object
            XRGrabInteractable interactable = this.GetComponent<XRGrabInteractable>();

            // We iterate over the interactors that are currently interacting with this object and its state is selecting 
            foreach (IXRSelectInteractor interactor in interactable.interactorsSelecting)
            {
                // We cast the selecing interactor as a generic controller interactor, either the left or right controller
                XRBaseControllerInteractor controllerInteractor = interactor as XRBaseControllerInteractor;

                if (controllerInteractor != null)
                {
                    // We get the actual controller that is performing the interaction (left and right) and we apply haptic vibration
                    // with the parametrized intensity from the affordance theme 
                    controllerInteractor.xrController.SendHapticImpulse(intensity, 0.5f);
                }
            }

            //Similarly we can implement the same logic to other interaction states in the interactable, i.e. hovering interaction
            foreach (IXRHoverInteractor interactor in interactable.interactorsHovering)
            {
                XRBaseControllerInteractor controllerInteractor = interactor as XRBaseControllerInteractor;

                if (controllerInteractor != null)
                    controllerInteractor.xrController.SendHapticImpulse(intensity, 0.1f);
            }


        }
    }
}
