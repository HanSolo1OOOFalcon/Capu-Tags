using Locomotion;
using TMPro;
using UnityEngine;

namespace CapuTags
{
    public class NametagComponent : MonoBehaviour
    {
        // Making these variables public if someone would like to make an addon for CapuTags! (If you're reading this and making an addon, you're welcome and good luck)
        public FusionPlayer player;
        public GameObject nametag;
        public TextMeshPro nametagText;

        void Start()
        {
            nametag = new GameObject("CapuTagsNametag");
            nametag.transform.SetParent(transform);
            nametag.transform.localPosition = new Vector3(0f, 0.5f, 0f); 
            nametagText = nametag.AddComponent<TextMeshPro>();
            nametagText.fontSize = 3f;
            nametagText.material.shader = Shader.Find("UI/Default");
            nametagText.alignment = TextAlignmentOptions.Center;
        }

        void OnDisable()
        {
            if (nametag != null)
            {
                Destroy(nametag);
                nametag = null;
            }
        }

        void Update()
        {
            if (nametag != null)
            {
                nametag.transform.LookAt(Player.Instance.playerCam.gameObject.transform);
                nametag.transform.Rotate(0, 180, 0);
                
                nametagText.text = player.Username;
                nametagText.color = player.__Color;
            }
        }
    }
}
