using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System;

public class LSystemsGenerator : MonoBehaviour
{
    public static int NUM_OF_TREES = 9;
    public static int MAX_ITERATIONS = 7;

    public int title = 1;
    public int iterations;
    public float angle;
    public float width;
    public float length;
    public float variance = 20f;

    public bool hasTreeChanged = false;
    public GameObject Tree = null;

    [SerializeField] private GameObject treeParent;
    [SerializeField] private GameObject branch;
    [SerializeField] private GameObject leaf;
    [SerializeField] private GUIScript GUI;

    private const string axiom1 = "X";
    private const string axiom2 = "F";

    private Dictionary<char, string> rules;
    private Stack<TransformInfo> transformStack;
    private int titleLastFrame;
    private int iterationsLastFrame;
    private float angleLastFrame;
    private float widthLastFrame;
    private float lengthLastFrame;
    private string currentString = string.Empty;
    private Vector3 initialPosition = Vector3.zero;
    private float[] randomRotationValues = new float[100];

    public class TransformInfo
    {
        public Vector3 position;
        public Quaternion rotation;
    }


    private void Start()
    {
        titleLastFrame = title;
        iterationsLastFrame = 5;
        angleLastFrame = 25.7f;
        widthLastFrame = 1f;
        lengthLastFrame = 1f;

        for (int i = 0; i < randomRotationValues.Length; i++)
        {
            randomRotationValues[i] = UnityEngine.Random.Range(-1f, 1f);
        }

        transformStack = new Stack<TransformInfo>();

         
        rules = new Dictionary<char, string>
        {
            { 'A',"F"},
            { 'F', "F[+F]F[-F]F" },
        };
       
        Generate();
    }

    private void Update()
    {

        
        if (GUI.hasGenerateBeenPressed)
        {
            ResetRandomValues();
            GUI.hasGenerateBeenPressed = false;
            Generate();
        }

        if (GUI.hasResetBeenPressed)
        {
            ResetTreeValues();
            GUI.hasResetBeenPressed = false;
            GUI.Start();
            Generate();
        }


        GUI.rotation.gameObject.SetActive(true);


        
        if (titleLastFrame != title)
        {
            
            if (title >= 7)
            {
                GUI.rotation.gameObject.SetActive(true);
            }
            else
            {
                GUI.rotation.value = 0;
                GUI.rotation.gameObject.SetActive(false);
            }

            
            switch (title)
            {
                case 1:
                    SelectTreeOne();
                    break;

                case 2:
                    SelectTreeTwo();
                    break;

                case 3:
                    SelectTreeThree();
                    break;

                case 4:
                    SelectTreeFour();
                    break;

                case 5:
                    SelectTreeFive();
                    break;

                case 6:
                    SelectTreeSix();
                    break;

                case 7:
                    SelectTreeSeven();
                    break;

                case 8:
                    SelectTreeEight();
                    break;

                case 9:
                    SelectTreeNine();
                    break;

                default:
                    SelectTreeOne();
                    break;
            }

            titleLastFrame = title;
        }
        

        
        if (iterationsLastFrame != iterations || angleLastFrame  != angle || widthLastFrame  != width || lengthLastFrame != length)
        {
            ResetFlags();
            Generate();
        }
        

    }

    private void Generate()
    {
        Destroy(Tree);

        Tree = Instantiate(treeParent);

        gameObject.transform.position = new Vector3(0, 0, 0);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

        var A1 = rules['A'];

        Debug.Log(A1);

        if (A1 == "X")
        {
            currentString = axiom1;
        }
        else
        {
            currentString = axiom2;
        };


        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < iterations; i++)
        {
            foreach (char c in currentString)
            {
                sb.Append(rules.ContainsKey(c) ? rules[c] : c.ToString());
            }


            currentString = sb.ToString();
            sb = new StringBuilder();
        }

        Debug.Log(currentString);
        
        for (int i = 0; i < currentString.Length; i++)
        {
            switch (currentString[i])
            {
                case 'F':                    
                    initialPosition = transform.position;
                    transform.Translate(Vector3.up * 2 * length);                    

                    GameObject fLine = currentString[(i + 1) % currentString.Length] == 'X' || currentString[(i + 3) % currentString.Length] == 'F' && currentString[(i + 4) % currentString.Length] == 'X' ? Instantiate(leaf) : Instantiate(branch);
                    fLine.transform.SetParent(Tree.transform);
                    fLine.GetComponent<LineRenderer>().SetPosition(0, initialPosition);
                    fLine.GetComponent<LineRenderer>().SetPosition(1, transform.position);
                    fLine.GetComponent<LineRenderer>().startWidth = width;
                    fLine.GetComponent<LineRenderer>().endWidth = width;
                    break;

                case 'X':                
                    break;
              
                case '+':
                    transform.Rotate(Vector3.forward * angle * (1 + variance / 100 * randomRotationValues[i % randomRotationValues.Length]));
                    break;

                case '-':                                      
                    transform.Rotate(Vector3.back * angle * (1 + variance / 100 * randomRotationValues[i % randomRotationValues.Length]));
                    break;

                case '*':
                    transform.Rotate(Vector3.up * 120 * (1 + variance / 100 * randomRotationValues[i % randomRotationValues.Length]));
                    break;

                case '/':
                    transform.Rotate(Vector3.down * 120 * (1 + variance / 100 * randomRotationValues[i % randomRotationValues.Length]));
                    break;

                case '[':
                    transformStack.Push(new TransformInfo()
                    {
                        position = transform.position,
                        rotation = transform.rotation
                    });
                    break;

                case ']':
                    TransformInfo ti = transformStack.Pop();
                    transform.position = ti.position;
                    transform.rotation = ti.rotation;
                    break;

                default:
                    
                    break;

            }
        }

        Tree.transform.rotation = Quaternion.Euler(0, GUI.rotation.value, 0);
    }

    private void SelectTreeOne()
    {
        rules = new Dictionary<char, string>
        {
            { 'A',"F"},
            { 'F', "F[+F]F[-F]F" },
        };

        Generate();
    }

    private void SelectTreeTwo()
    {
        rules = new Dictionary<char, string>
        {
            { 'A',"F"},
            { 'F', "F[+F]F[-F][F]" },
        };

        Generate();
    }

    private void SelectTreeThree()
    {
        rules = new Dictionary<char, string>
        {
            { 'A',"F"},
            { 'F', "FF-[-F+F+F]+[+F-F-F]" },
        };

        Generate();
    }

    private void SelectTreeFour()
    {

        rules = new Dictionary<char, string>
        {
            { 'A',"X"},
            { 'X', "F[+X]F[-X]+X" }, 
            { 'F', "FF" }
        };

        Generate();
    }

    private void SelectTreeFive()
    {
        rules = new Dictionary<char, string>
        {
            { 'A',"X"},
            { 'X', "F[+X][-X]FX" },
            { 'F', "FF" }
        };

        Generate();
    }

    private void SelectTreeSix()
    {
        rules = new Dictionary<char, string>
        {
            { 'A',"X"},
            { 'X', "F-[[X]+X]+F[+FX]-X" },
            { 'F', "FF" }
        };

        Generate();
    }

    private void SelectTreeSeven()
    {
        rules = new Dictionary<char, string>
        {
            { 'A',"X"},
            { 'X', "[*+FX]X[+FX][/+F-FX]" },
            { 'F', "FF" }
        };

        Generate();
    }

    private void SelectTreeEight()
    {
        rules = new Dictionary<char, string>
        {
            { 'A',"X"},
            { 'X', "[F[-X+F[+FX]][*-X+F[+FX]][/-X+F[+FX]-X]]" },
            { 'F', "FF" }
        };

        Generate();
    }

    private void SelectTreeNine()
    {
        rules = new Dictionary<char, string>
        {
            { 'A',"X"},
            { 'X', "[F[+FX][*+FX][/+FX]]" },
            { 'F', "FF" }
        };


        Generate();
    }

    private void ResetRandomValues()
    {
        for (int i = 0; i < randomRotationValues.Length; i++)
        {
            randomRotationValues[i] = UnityEngine.Random.Range(-1f, 1f);
        }
    }

    private void ResetFlags()
    {
        iterationsLastFrame = iterations;
        angleLastFrame = angle;
        widthLastFrame = width;
        lengthLastFrame = length;
    }

    private void ResetTreeValues()
    {
        iterations = 4;
        angle = 30f;
        width = 1f;
        length = 2f;
        variance = 20f;
    }

    
   
}