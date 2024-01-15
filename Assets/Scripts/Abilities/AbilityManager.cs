using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class AbilitySpritePair
{ 
    public Ability ability;
    public Sprite sprite;
}

public class AbilityManager : MonoBehaviour
{
    [SerializeField]
    GameObject m_abilityScreen;

    [SerializeField]
    Image m_abilityOne;
    [SerializeField]
    Image m_abilityTwo;
    [SerializeField]
    Image m_abilityThree;

    public static AbilityManager Instance;

    List<IAbility> m_abilities = new();

    [field: SerializeField]
    private List<AbilitySpritePair> m_abilitySprites;

    IAbility[] m_listIndexOfRandomAbilities = new IAbility[3];
    bool m_notChosen = false;

    private void Awake()
    {
        Instance = this;

        m_abilities.Add(new AbilityDamageMultiplier());
        m_abilities.Add(new AbilityHealthDrop());
        m_abilities.Add(new AbilitySpeed());
        m_abilities.Add(new AbilityDefense());
    }

    public void RemoveAbility(IAbility ability)
    {
        m_abilities.Remove(ability);
    }

    public void ApplyOne()
    {
        m_listIndexOfRandomAbilities[0].ApplyAbility();
        m_notChosen = false;
    }

    public void ApplyTwo() 
    {
        m_listIndexOfRandomAbilities[1].ApplyAbility();
        m_notChosen = false;
    }

    public void ApplyThree()
    {
        m_listIndexOfRandomAbilities[2].ApplyAbility();
        m_notChosen = false;
    }

    public void GivePlayerAbilityChoice()
    {
        if(m_abilities.Count == 0) 
        { 
            return; 
        }
        else if (m_abilities.Count > 3)
        {

        } else if (m_abilities.Count == 3)
        {

        }

        switch(m_abilities.Count)
        {
        case 0:
            return;
        case 1:
            OneChoiceRemaining();
            break;
        case 2:
            TwoChoiceRemaining();
            break;
        case 3:
            ThreeChoicesRemaining();
            break;
        default:
            RandomThreeChoices();
            break;
        }
        

        StartCoroutine(AbilityChoice());
    }

    private void RandomThreeChoices()
    {
        for (int i = 0; i < m_listIndexOfRandomAbilities.Length; i++)
        {
            bool valid = true;
            int e = 0;
            int rand;
            do
            {
                rand = Random.Range(0, m_abilities.Count);

                for (int j = 0; j < i; j++)
                {
                    if ((int)m_listIndexOfRandomAbilities[j].GetAbilityType() == rand)
                    {
                        valid = false;
                    }
                }
            } while (!valid && e++ < 1000);

            m_listIndexOfRandomAbilities[i] = m_abilities[rand];
        }

        m_abilityOne.sprite =   m_abilitySprites[(int)m_listIndexOfRandomAbilities[0].GetAbilityType()].sprite;
        m_abilityTwo.sprite =   m_abilitySprites[(int)m_listIndexOfRandomAbilities[1].GetAbilityType()].sprite;
        m_abilityThree.sprite = m_abilitySprites[(int)m_listIndexOfRandomAbilities[2].GetAbilityType()].sprite;
    }

    private void ThreeChoicesRemaining()
    {
        for(int i = 0; i < 3; i++)
        {
            m_listIndexOfRandomAbilities[i] = m_abilities[i];
        }

        m_abilityOne.sprite = m_abilitySprites[(int)m_abilities[0].GetAbilityType()].sprite;
        m_abilityTwo.sprite = m_abilitySprites[(int)m_abilities[1].GetAbilityType()].sprite;
        m_abilityThree.sprite = m_abilitySprites[(int)m_abilities[2].GetAbilityType()].sprite;
    }

    private void TwoChoiceRemaining()
    {
        m_listIndexOfRandomAbilities[0] = m_abilities[0];
        m_listIndexOfRandomAbilities[2] = m_abilities[1];

        m_abilityOne.sprite = m_abilitySprites[(int)m_abilities[0].GetAbilityType()].sprite;
        m_abilityTwo.gameObject.SetActive(false);
        m_abilityThree.sprite = m_abilitySprites[(int)m_abilities[1].GetAbilityType()].sprite;
    }

    private void OneChoiceRemaining()
    {
        m_listIndexOfRandomAbilities[1] = m_abilities[0];

        m_abilityOne.gameObject.SetActive(false);
        m_abilityTwo.gameObject.SetActive(true);
        m_abilityThree.gameObject.SetActive(false);

        m_abilityTwo.sprite = m_abilitySprites[(int)m_abilities[0].GetAbilityType()].sprite;
    }

    IEnumerator AbilityChoice()
    {
        m_notChosen = true;

        m_abilityScreen.SetActive(true);
        Time.timeScale = 0.0f;

        while (m_notChosen)
        {
            yield return null;
        }

        Time.timeScale = 1.0f;
        m_abilityScreen.SetActive(false);
    }
}
