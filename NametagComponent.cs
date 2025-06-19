using Locomotion;
using TMPro;
using UnityEngine;

namespace CapuTags
{
    public class NametagComponent : MonoBehaviour
    {
        public string playerName;
        public Color playerColor;
        private GameObject nametag;

        void Start()
        {
            nametag = new GameObject("Nametag");
            nametag.transform.SetParent(transform);
            nametag.transform.localPosition = new Vector3(0f, 0.5f, 0f);
            nametag.transform.localRotation = Quaternion.identity;
            TextMeshPro nametagText = nametag.AddComponent<TextMeshPro>();
            nametagText.text = playerName;
            nametagText.fontSize = 3f;
            nametagText.color = playerColor;
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
            }
        }
    }
}
