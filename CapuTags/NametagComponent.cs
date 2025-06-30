using Il2Cpp;
using Il2CppLocomotion;
using Il2CppTMPro;
using UnityEngine;

namespace CapuTags
{
    public class NametagComponent : MonoBehaviour
    {
        // Make addons is cool.
        public FusionPlayer player;
        public GameObject nametag;
        public TextMeshPro nametagText;
        public Action OnTextChanged, OnColorChanged;
        private Color lastColor;
        private string lastName;

        void Start()
        {
            nametag = new GameObject("CapuTagsNametag");
            nametag.transform.SetParent(transform);
            nametag.transform.localPosition = new Vector3(0f, 0.005f, 0f);
            nametagText = nametag.AddComponent<TextMeshPro>();
            nametagText.fontSize = 2f;
            nametagText.material.shader = Shader.Find("UI/Default");
            nametagText.alignment = TextAlignmentOptions.Center;
            nametagText.text = player.Username;
            nametagText.color = player.__Color;
            nametagText.font = GameObject.Find("Global/Levels/ObjectNotInMaps/Stump/TableOffset/QueueBoard/Text (TMP)").GetComponent<TextMeshPro>().font;
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
                Transform nametagTransform = nametag.transform;
                nametagTransform.rotation = Quaternion.LookRotation(
                    Player.Instance.playerCam.transform.position - nametagTransform.position,
                    Player.Instance.playerCam.transform.up
                );
                nametagTransform.Rotate(0f, 180f, 0f);

                string name = player.Username;
                if (name != lastName)
                {
                    nametagText.text = name;
                    lastName = name;
                    OnTextChanged?.Invoke();
                }

                Color color = player.__Color;
                if (color != lastColor)
                {
                    nametagText.color = color;
                    lastColor = color;
                    OnColorChanged?.Invoke();
                }
            }
        }
    }
}