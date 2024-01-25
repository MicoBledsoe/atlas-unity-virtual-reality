using UnityEngine;
using UnityEngine.UI;

public class SkyboxChanger : MonoBehaviour
{
    public Material Cloudymorning;
    public Material CloudedSunGlow;
    private Dropdown dropdown;

    void Start()
    {
        dropdown = GetComponent<Dropdown>();
        dropdown.onValueChanged.AddListener(delegate { ChangeSkybox(dropdown.value); });
    }

    void ChangeSkybox(int index)
    {
        if (index == 0)
        {
            RenderSettings.skybox = Cloudymorning;
        }
        else if (index == 1)
        {
            RenderSettings.skybox = CloudedSunGlow;
        }

        // Update global illumination to match the new skybox
        DynamicGI.UpdateEnvironment();
    }
}
