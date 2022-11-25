using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIScript : MonoBehaviour
{
    public bool hasGenerateBeenPressed = false;
    public bool hasResetBeenPressed = false;
    public Slider rotation;


    [SerializeField] private LSystemsGenerator TreeBuilder;
    [SerializeField] private InputField title;
    [SerializeField] private InputField iterations;
    [SerializeField] private InputField angle;
    [SerializeField] private InputField length;
    [SerializeField] private InputField width;

   
    [SerializeField] private InputField variance;
   

   //private int tempInt;
   //private float tempFloat;

   public void Start ()
   {
       title.text = TreeBuilder.title.ToString();
       iterations.text = TreeBuilder.iterations.ToString();
       angle.text = TreeBuilder.angle.ToString() + "°";
       length.text = TreeBuilder.length.ToString("F1");
       width.text = TreeBuilder.width.ToString("F1");

       variance.text = TreeBuilder.variance.ToString() + "%";

       rotation.gameObject.SetActive(false);

   }

   public void TitleUp()
   {
       if (TreeBuilder.title < LSystemsGenerator.NUM_OF_TREES)
       {
            TreeBuilder.title++;
            TreeBuilder.hasTreeChanged = true;
            title.text = TreeBuilder.title.ToString();
       }
   }
   public void TitleDown()
   {
       if (TreeBuilder.title > 1)
       {
            TreeBuilder.title--;
            TreeBuilder.hasTreeChanged = true;
            title.text = TreeBuilder.title.ToString();
       }
   }

   public void IterationsUp()
   {
       if (TreeBuilder.iterations < LSystemsGenerator.MAX_ITERATIONS)
       {
            TreeBuilder.iterations++;
            iterations.text = TreeBuilder.iterations.ToString();
       }
   }
   public void IterationsDown()
   {
       if (TreeBuilder.iterations > 1)
       {
            TreeBuilder.iterations--;
           iterations.text = TreeBuilder.iterations.ToString();
       }
   }

   public void AngleUp()
   {
        TreeBuilder.angle++;
        angle.text = TreeBuilder.angle.ToString() + "°";
   }
   public void AngleDown()
   {
        TreeBuilder.angle--;
        angle.text = TreeBuilder.angle.ToString() + "°";
   }

   public void LengthUp()
   {
        TreeBuilder.length += .1f;
        length.text = TreeBuilder.length.ToString("F1");
   }
   public void LengthDown()
   {
       if (TreeBuilder.length > 0)
       {
            TreeBuilder.length -= .1f;
           length.text = TreeBuilder.length.ToString("F1");
       }
   }

   public void WidthUp()
   {
        TreeBuilder.width += .1f;
       width.text = TreeBuilder.width.ToString("F1");
   }
   public void WidthDown()
   {
       if (TreeBuilder.width > 0)
       {
            TreeBuilder.width -= .1f;
           width.text = TreeBuilder.width.ToString("F1");
       }
   }


   
   public void VarianceUp()
   {
        TreeBuilder.variance++;
        variance.text = TreeBuilder.variance.ToString() + "%";
   }
   public void VarianceDown()
   {
       if (TreeBuilder.variance > 0)
       {
            TreeBuilder.variance--;
            variance.text = TreeBuilder.variance.ToString() + "%";
       }
   }
   

    public void GenerateNew()
    {
        hasGenerateBeenPressed = true;
    }

    public void ResetValues()
    {
        hasResetBeenPressed = true;        
    }

    public void RotateTree()
    {
        TreeBuilder.Tree.transform.rotation = Quaternion.Euler(0, rotation.value, 0);
    }


}
