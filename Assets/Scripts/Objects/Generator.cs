using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEditor.Rendering.Universal;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public List<LostObject> lostObjects;
    public List<Person> people;
    public ObjectDatabase db;

    List<string> dunno = new List<string>()
        {
            "It's definitely... something I fail to recollect.",
            "I think it's... something I don't remember",
            "I'm quite sure it's an information that could help! If I knew it.",
            "I'm keen to say I don't know it.",
            "I don't remember that detail",
            "Mmm... It's hard to remember that.",
            "Argh! I know this! It's... Ehm... It's on the tip of my tongue...",
        };

    List<string> notAvailable = new List<string>()
        {
            "What are you asking?",
            "Are you for real?",
            "Are you kidding me?",
            "Are you joking me?",
            "This must be a joke.",
            "I see you like being funny. Do you have a license for that?",
            "Amusing question. Anything else?",
            "kek",
            "lol",
            "lmao",
            "Why are you asking me that?",
            "Yes. But also no."

        };

    List<string> preColor = new List<string>()
        {
            "It's definitely ",
            "I think it's ",
            "I'm quite sure it's ",
            "I'm keen to say it was ",
            "I remember it looked ",
            "Mmm... It's ",
            "I know this! It's ",
            "I can assess it is ",
            "Well, I recall it's ",

        };

    List<string> preSex = new List<string>()
        {
            "It's definitely a ",
            "I think it's a ",
            "I'm quite sure it's a ",
            "I'm keen to say it is a ",
            "I remember it looked like a ",
            "Mmm... It's a ",
            "I know this! It's a ",
            "I can assess it is a ",
            "Well, I recall it's a ",
        };

    List<string> preSpecies = new List<string>()
        {
            "I think it belongs to the ",
            "I believe it is part of the ",
            "Some scientists would say it should be grouped with the ",
            "Uhh, I think it's part of the ",
            "I remember it's definitely in the ",
            "Mmm... It's part of the ",
            "I know this! It belongs to the ",
            "I can say it should be considered part of the ",
            "Well, I... admit it is part of the ",
        };

    List<string> preEdible = new List<string>()
        {
            "Mmm... ",
            "Well, ",
            "To be fair, ",
            "In my humble opinion, ",
            " ",
            "Is it edible? I would say... ",
            "I know this! The answer is ",
            "Oh, I guess I can say ",
            "I'm not totally sure, but I'm going to say ",
        };

    List<string> preOrigin = new List<string>()
        {
            "It comes from ",
            "It is from ",
            "I believe it was made in ",
            "I think it's from ",
            "I believe it originates in ",
            "Where is from? ",
            "I know this! The answer is ",
            "Oh, I recall it is from ",
            "I'm not totally sure, but I'm going to say ",
        };


    // Start is called before the first frame update
    void Start()
    {
        InitializeObject(3);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitializeObject(int numPeople)
    {
        lostObjects.Clear();
        people.Clear();
        ObjectDefinition randomDef = db.objects[Random.Range(0, db.objects.Count)];
        LostObject lObj = new LostObject();
        lObj.name = randomDef.name;
        lObj.properties = new LostObjectPropertiesDict();
        foreach (KeyValuePair<ObjectProperty, PropertyDetail> entry in randomDef.properties)
        {
            if (entry.Value.type == PropertyType.RANDOM_STRING)
            {
                lObj.properties.Add(entry.Key, entry.Value.values[Random.Range(0, entry.Value.values.Count)]);
            }
            else if (entry.Value.type == PropertyType.RANGE)
            {
                CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
                ci.NumberFormat.CurrencyDecimalSeparator = ".";
                lObj.properties.Add(entry.Key, Random.Range(float.Parse(entry.Value.values[0], NumberStyles.Any, ci), float.Parse(entry.Value.values[1], NumberStyles.Any, ci)).ToString("0.00"));
            }
        }
        lostObjects.Add(lObj);
        AddPeople(lObj, numPeople);
    }

    // TO DO - Shuffle the list of people
    void AddPeople(LostObject lostObject, int num = 2)
    {
        if (num < 2) num = 2;
        Person p = new Person();
        p.isLegitOwner = true;
        p.answers = new LostObjectPropertiesDict();
        ObjectProperty truth = lostObject.properties.Keys.ToArray()[Random.Range(0, lostObject.properties.Count)];
        string nA = "N/A";
        while (lostObject.properties[truth] == nA) 
        {
            truth = lostObject.properties.Keys.ToArray()[Random.Range(0, lostObject.properties.Count)];
        }
        foreach (KeyValuePair<ObjectProperty, string> entry in lostObject.properties)
        {
            if (entry.Key == truth) 
            {
                p.answers[entry.Key] = GenerateOwnerAnswer(entry.Key, entry.Value, true);
            }
            else 
            {
                if (entry.Value != nA)
                {
                    p.answers[entry.Key] = GenerateOwnerAnswer(entry.Key, entry.Value);
                } 
                else
                {
                    p.answers[entry.Key] = notAvailable[Random.Range(0, notAvailable.Count)];
                }
            }
        }
        people.Add(p);

        for (int i = 0; i < num-1; i++) 
        {
            Person ap = new Person();
            ap.isLegitOwner = false;
            ap.answers = new LostObjectPropertiesDict();
            ObjectProperty lie = lostObject.properties.Keys.ToArray()[Random.Range(0, lostObject.properties.Count)];
            while (lostObject.properties[lie] == nA)
            {
                lie = lostObject.properties.Keys.ToArray()[Random.Range(0, lostObject.properties.Count)];
            }
            foreach (KeyValuePair<ObjectProperty, string> entry in lostObject.properties)
            {
                if (entry.Key == lie)
                {
                    ap.answers[entry.Key] = GenerateThiefAnswer(entry.Key, entry.Value, true);
                }
                else
                {
                    if (entry.Value != nA)
                    {
                        ap.answers[entry.Key] = GenerateThiefAnswer(entry.Key, entry.Value);
                    }
                    else
                    {
                        ap.answers[entry.Key] = notAvailable[Random.Range(0, notAvailable.Count)];
                    }
                }
            }
            people.Add(ap);
        }
    }

    string GenerateOwnerAnswer(ObjectProperty op, string s, bool rightAnswer=false) 
    {

        string answer = "";
        int rand = Random.Range(0, 2);
        
        if (rightAnswer || rand == 0) 
        { 
            if (op == ObjectProperty.COLOR) 
            {
                answer = preColor[Random.Range(0, preColor.Count)] + s;
            }
            if (op == ObjectProperty.WEIGHT)
            {
                rand = Random.Range(0, 3);
                if (rand == 0)
                {
                    answer = "It weights less than " + Mathf.Clamp((float.Parse(s) + Random.Range(1, 10)), 1, 9000) + " kg";
                }
                if (rand == 1)
                {
                    answer = "It weights more than " + Mathf.Clamp((float.Parse(s) - Random.Range(1, 10)), 1, 9000) + " kg";
                }
                if (rand == 2)
                {
                    answer = "It weights around " + float.Parse(s) + " kg";
                }
            }
            if (op == ObjectProperty.HEIGHT)
            {
                rand = Random.Range(0, 3);
                if (rand == 0)
                {
                    answer = "It is shorter than " + Mathf.Clamp((float.Parse(s) + Random.Range(5, 10)), 1, 9000) + " cm";
                }
                if (rand == 1)
                {
                    answer = "It is taller than " + Mathf.Clamp((float.Parse(s) - Random.Range(5, 10)), 1, 9000) + " cm";
                }
                if (rand == 2)
                {
                    answer = "It is " + float.Parse(s) + " cm tall";
                }
            }
            if (op == ObjectProperty.SEX)
            {
                answer = preSex[Random.Range(0, preSex.Count)] + s;
            }
            if (op == ObjectProperty.SPECIES)
            {
                answer = preSpecies[Random.Range(0, preSpecies.Count)] + s + " species";
            }
            if (op == ObjectProperty.EDIBLE)
            {
                answer = preEdible[Random.Range(0, preEdible.Count)] + s;
            }
            if (op == ObjectProperty.AGE)
            {
                rand = Random.Range(0, 3);
                if (rand == 0)
                {
                    answer = "It is less old than " + Mathf.Clamp((float.Parse(s) + Random.Range(5, 10)), 1, 9000) + " years";
                }
                if (rand == 1)
                {
                    answer = "It's been around for more than " + Mathf.Clamp((float.Parse(s) - Random.Range(5, 10)), 1, 9000) + " years";
                }
                if (rand == 2)
                {
                    answer = "It is " + float.Parse(s) + " years old";
                }
            }
            if (op == ObjectProperty.ORIGIN)
            {
                answer = preOrigin[Random.Range(0, preOrigin.Count)] + s;
            }
        }
        else 
        {
            answer = dunno[Random.Range(0, dunno.Count)];
        }
        return answer;
    }

    string GenerateThiefAnswer(ObjectProperty op, string s, bool isLyingForSure=false)
    {
        string answer = "";
        string ans = "";
        int rand = Random.Range(0, 3);
        if (rand == 0 || isLyingForSure)
        {
            if (db.propertiesValues[op].values.Count > 0) // number property won't have propertiesValues so we have to check if the property is number or string
            {
                ans = db.propertiesValues[op].values[Random.Range(0, db.propertiesValues[op].values.Count)];
                while (ans == s)
                {
                    ans = db.propertiesValues[op].values[Random.Range(0, db.propertiesValues[op].values.Count)];
                }
            }

            switch (op) 
            {
                case ObjectProperty.COLOR:
                    answer = preColor[Random.Range(0, preColor.Count)] + ans;
                    break;
                case ObjectProperty.WEIGHT:
                    {
                        rand = Random.Range(0, 2);
                        if (rand == 0)
                        {
                            answer = "It weights more than " + Mathf.Clamp((float.Parse(s) + Random.Range(1, 10)), 1, 9000) + " kg";
                        }
                        if (rand == 1)
                        {
                            answer = "It weights less than " + Mathf.Clamp((float.Parse(s) - Random.Range(1, 10)), 1, 9000) + " kg";
                        }
                    }
                    break;
                case ObjectProperty.HEIGHT:
                    if (rand == 0)
                    {
                        answer = "It is taller than " + Mathf.Clamp((float.Parse(s) + Random.Range(5, 10)), 1, 9000) + " cm";
                    }
                    if (rand == 1)
                    {
                        answer = "It is shorter than " + Mathf.Clamp((float.Parse(s) - Random.Range(5, 10)), 1, 9000) + " cm";
                    }
                    break;
                case ObjectProperty.SEX:
                    answer = preSex[Random.Range(0, preSex.Count)] + ans;
                    break;
                case ObjectProperty.SPECIES:
                    answer = preSpecies[Random.Range(0, preSpecies.Count)] + ans;
                    break;
                case ObjectProperty.EDIBLE:
                    answer = preEdible[Random.Range(0, preEdible.Count)] + ans;
                    break;
                case ObjectProperty.AGE:
                    if (rand == 0)
                    {
                        answer = "It's been around for more than " + Mathf.Clamp((float.Parse(s) + Random.Range(5, 10)), 1, 9000) + " years";
                    }
                    if (rand == 1)
                    {
                        answer = "It is less old than " + Mathf.Clamp((float.Parse(s) - Random.Range(5, 10)), 1, 9000) + " years";
                    }
                    break;
                case ObjectProperty.ORIGIN:
                    answer = preOrigin[Random.Range(0, preOrigin.Count)] + ans;
                    break;
            }
        }
        else if (rand == 1)
        {
            answer = dunno[Random.Range(0, dunno.Count)];
        }
        else if (rand == 2)
        {
            answer = GenerateOwnerAnswer(op, s, true);
        }
        return answer;
    }
}
