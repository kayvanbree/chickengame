using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Vector3 m_Move;
		public int playerIdx = -1;
        
        private void Start()
        {
            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();
        }

        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            // read inputs
            float h = GamepadManager.Instance.GetGamepad(playerIdx).GetStick_L().X; 
			float v = GamepadManager.Instance.GetGamepad(playerIdx).GetStick_L().Y; 

			// we use world-relative directions in the case of no main camera
			m_Move = v*Vector3.forward + h*Vector3.right;

            // pass all parameters to the character control script
            m_Character.Move(m_Move);
        }
    }
}
