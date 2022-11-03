using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [Header("UI Elements")]
    [Header("Sliders and indicators")]
    public Slider planetSlider;
    public TextMeshProUGUI planetAmountIndicatorText;
    public Slider moonSlider;
    public TextMeshProUGUI moonAmountIndicatorText;
    [Header("Toggles")]
    public Toggle elliptical;
    [Header("Input fields")]
    public TMP_InputField radiusIncrement;
    public TMP_InputField maxSpeed;
    public TMP_InputField minSpeed;
    [Header("Buttons")]
    public Button applyButton;
    public Button resetButton;
    [Header("Warning UI")]
    public TextMeshProUGUI warningText;
    [Space]

    [Header("Sun transform")]
    public Transform sun;
    [Space]

    [Header("String arrays for randomly generated names")]
    [SerializeField] private string[] prefixes;
    [SerializeField] private string[] postfixes;

    [Header("Prefabs")]
    public GameObject planetPrefab;
    public GameObject moonPrefab;

    //list to keep everything so we can destroy it later
    private List<GameObject> orbitals = new List<GameObject>();

    private void Start()
    {
        //make sure the display text updates properly
        moonSlider.onValueChanged.AddListener((float value) => moonAmountIndicatorText.text = ((int)value).ToString());
        planetSlider.onValueChanged.AddListener((float value) => planetAmountIndicatorText.text = ((int)value).ToString());

        //disable warning text (remember it's a child of a panel)
        warningText.transform.parent.gameObject.SetActive(false);

        applyButton.onClick.AddListener(ApplySettings);
        resetButton.onClick.AddListener(ResetSettings);
    }

    public void ApplySettings()
    {
        //check a bunch of stuff first
        //if planets are 0, dont do anything
        if (planetSlider.value == 0)
        {
            //maybe add a warning here
            return;
        }

        float radInc = float.Parse(radiusIncrement.text);
        if (radInc <= 0)
        {
            //warning for <0 radius
            return;
        }
        float m_maxSpeed = float.Parse(maxSpeed.text) * 0.1f; 
        float m_minSpeed = float.Parse(minSpeed.text) * 0.1f;

        if (m_maxSpeed <= 0 || m_minSpeed <= 0 || (m_maxSpeed < m_minSpeed))
        {
            //warning about speeds
            return;
        }

        float xMod = 1;
        float zMod = 1;
        //instantiate all planets, give them the sun as their orbit center,
        //give a random(?) radius, offset and orientation
        for (int i = 0; i < planetSlider.value; i++)
        {
            GameObject tempPlanet = Instantiate(planetPrefab);
            tempPlanet.name = string.Format("Planet {0}{1}",
                prefixes[Random.Range(0, prefixes.Length - 1)], postfixes[Random.Range(0, postfixes.Length - 1)]);
            Orbit planetOrbit = tempPlanet.GetComponent<Orbit>();
            if (elliptical.isOn)
            {
                xMod += Random.Range(0.1f, 1f);
                zMod += Random.Range(0.1f, 1f);
            }

            planetOrbit.Initialize(sun, Random.rotation.eulerAngles, Vector3.zero,
               radInc * (i + 1), xMod, zMod, Random.Range(m_minSpeed, m_maxSpeed), Random.Range(0, 360f));

            for (int j = 0; j < moonSlider.value; j++)
            {
                GameObject tempMoon = Instantiate(moonPrefab);
                tempMoon.name = string.Format("Moon {0}{1} of {2}", 
                    prefixes[Random.Range(0, prefixes.Length - 1)], postfixes[Random.Range(0, postfixes.Length - 1)], tempPlanet.name);
                Orbit moonOrbit = tempMoon.GetComponent<Orbit>();
                moonOrbit.Initialize(tempPlanet.transform, Random.rotation.eulerAngles, Vector3.zero,
                        Random.Range(0.8f, 1.6f) * 3, xMod, zMod, Random.Range(m_minSpeed, m_maxSpeed) * 3, Random.Range(0, 360f));

                orbitals.Add(tempMoon);
            }
            orbitals.Add(tempPlanet);
        }

        //disable gameobject last
        gameObject.SetActive(false);
    }
    public void ResetSettings() 
    {
        for (int i = 0; i < orbitals.Count; i++)
        {
            Destroy(orbitals[i]);
        }
        orbitals.Clear();

        gameObject.SetActive(true);

        Updater.ResetUpdater();
    }

}

