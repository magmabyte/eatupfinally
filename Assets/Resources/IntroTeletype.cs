using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


//Please obey the rules and update the namespace of this MonoBehaviour IntroTeletype !

public class IntroTeletype : MonoBehaviour 
{
    public GameObject nextObject;

    public bool onOff = false;

    [Range(0, 100)]
    public int RevealSpeed = 50;

    private TMP_Text m_textMeshPro;

    public string label01 = "<size=72>AYCE</size> \n \n (all you can eat)... \n \n ...can you?";

    bool alltext = false;

    private void Awake()
    {
        m_textMeshPro = GetComponent<TMP_Text>();
        m_textMeshPro.text = label01;
    }

    // Use this for initialization
    IEnumerator Start()
    {

        // Force and update of the mesh to get valid information.
        m_textMeshPro.ForceMeshUpdate();


        int totalVisibleCharacters = m_textMeshPro.textInfo.characterCount; // Get # of Visible Character in text object
        int counter = 0;
        int visibleCount = 0;

        if (m_textMeshPro.maxVisibleCharacters != totalVisibleCharacters)
        { 
            while (!alltext)
            {
                visibleCount = counter % (totalVisibleCharacters + 1);

                m_textMeshPro.maxVisibleCharacters = visibleCount; // How many characters should TextMeshPro display?

                // Once the last character has been revealed, wait 1.0 second and start over.
                if (visibleCount >= totalVisibleCharacters)
                {

                    //yield return new WaitForSeconds(1.0f);
                    //m_textMeshPro.text = label01;
                    //yield return new WaitForSeconds(1.0f);
                }

                counter += 1;

                yield return new WaitForSeconds(RevealSpeed * 0.02f);

                if (m_textMeshPro.maxVisibleCharacters == totalVisibleCharacters)
                {
                    alltext = true;
                    if (nextObject != null)
                    {
                        nextObject.SetActive(true);
                    }
                }


            }
        }

        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
