using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {


	float hAxis, vAxis;
	public bool upAxisPressed, upAxisDown, downAxisPressed, downAxisDown, leftAxisPressed, leftAxisDown, rightAxisPressed, rightAxisDown;

	public string[] comboStack = new string[10]{null,null,null,null,null,null,null,null,null,null};
	int counterMoveStack=0;
	int frameMoveStack=0;
	int maxFrameMoveStack=30;

	public string[] punchCombo = new string[10]{"wPunch",null,null,null,null,null,null,null,null,null};
	public string[] kickCombo = new string[10]{"wKick",null,null,null,null,null,null,null,null,null};
	public string[] hadoukenCombo =  new string[10]{"down","forward","wPunch",null,null,null,null,null,null,null}; 
	public string[] shinkuHadoukenCombo = new string[10]{"down","forward","down","forward","wPunch",null,null,null,null,null};
	public string[] shoryukenCombo = new string[10]{"forward","down","forward","wPunch",null,null,null,null,null,null};
	public string[] hurricaneKickCombo = new string[10]{"down","back","wKick",null,null,null,null,null,null,null};

	//GETTERS & SETTERS

	public void changeOrientation()
	{
		transform.localScale = new Vector3(transform.localScale.x*-1, transform.localScale.y, transform.localScale.z);
	}

	public string[] getComboStack()
	{
		return this.comboStack;
	}
	public void setComboStack(string[] var)
	{
		this.comboStack = var;
	}
	public int getCounterMoveStack()
	{
		return counterMoveStack;
	}
	public void setCounterMoveStack(int var)
	{
		this.counterMoveStack = var;
	}
	public int getFrameMoveStack()
	{
		return frameMoveStack;
	}
	public void setFrameMoveStack(int var)
	{
		this.frameMoveStack = var;
	}
	public int getMaxFrameMoveStack()
	{
		return maxFrameMoveStack;
	}
	public void setMaxFrameMoveStack(int var)
	{
		this.maxFrameMoveStack = var;
	}
	public float getHAxis()
	{
		return this.hAxis;
	}
	public void setHAxis(float var)
	{
		this.hAxis = var;
	}
	public float getVAxis()
	{
		return this.vAxis;
	}
	public void setVAxis(float var)
	{
		this.vAxis = var;
	}


	/// <summary>
	/// Updates the state of the axis.
	/// </summary>
	public void updateAxisState()
	{
		hAxis = Input.GetAxis("Horizontal");	//Valor ejeHoriontal del controlador o teclado
		vAxis = Input.GetAxis("Vertical");		//Valor ejeVertical del controlador o teclado
		
		//Ejes Down
		if(vAxis==0)
		{
			upAxisPressed=false;
			downAxisPressed=false;
		}
		else if(vAxis < 0)
		{
			if(!downAxisPressed)	//Si el boton no estaba presionado entonces entendemos que se acaba de "pulsar" el eje
			{
				downAxisDown=true;
			}
			else
			{
				downAxisDown=false;
			}
			downAxisPressed=true;
			
		}
		else if (hAxis > 0)
		{
			if(!upAxisPressed)
			{
				upAxisDown=true;
			}
			else
			{
				upAxisDown=false;
			}
			upAxisPressed=true;
		}
		
		if (hAxis==0)
		{
			rightAxisPressed=false;
			leftAxisPressed=false;
		}
		else if (hAxis < 0 )
		{
			
			if(!leftAxisPressed)
			{
				leftAxisDown=true;
			}
			else
			{
				leftAxisDown=false;
			}
			leftAxisPressed=true;
			
		}
		else if(hAxis >0)
		{
			if(!rightAxisPressed)
			{
				rightAxisDown=true;
			}
			else
			{
				rightAxisDown=false;
			}
			rightAxisPressed=true;
		}
	}
	/// <summary>
	/// Updates the combo stack.
	/// </summary>
	public void updateComboStack()
	{
		//reset del combostack
		if (this.counterMoveStack > 9) 
		{
			this.counterMoveStack = 0;
		}

		//Stack de combos
		if (this.leftAxisDown) 
		{
			if (transform.localScale.x > 0)
			{
				this.comboStack[counterMoveStack]="back";
			}
			else
			{
				this.comboStack[counterMoveStack]="forward";
			}
			counterMoveStack++;
		}
		else if(this.rightAxisDown)
		{
			if (transform.localScale.x > 0)
			{
				this.comboStack[counterMoveStack]="forward";
			}
			else
			{
				this.comboStack[counterMoveStack]="back";
			}
			counterMoveStack++;
		}
		else if(this.upAxisDown)
		{
			this.comboStack[counterMoveStack]="up";
			counterMoveStack++;
		}
		else if(this.downAxisDown)
		{
			this.comboStack[counterMoveStack]="down";
			counterMoveStack++;
		}
		else if(Input.GetButtonDown("wPunch"))
		{
			this.comboStack[counterMoveStack]="wPunch";
			counterMoveStack++;
		}
		else if (Input.GetButtonDown ("wKick"))
		{
			this.comboStack[counterMoveStack]="wKick";
			counterMoveStack++;
		}

	}
	/// <summary>
	/// Equalses the combo array.
	/// </summary>
	/// <returns><c>true</c>, if combo array was equalsed, <c>false</c> otherwise.</returns>
	/// <param name="var">Variable.</param>
	bool equalsComboArray(string[] var)
	{

			for (int i = 0;i<comboStack.Length;i++)
			{
				
				if (comboStack[i]!=var[i])
				{
					return false;
				}
			}
			return true;

		

	}

	/// <summary>
	/// Checks the combo stack.
	/// </summary>
	/// <returns><c>true</c>, if combo stack was checked, <c>false</c> otherwise.</returns>
	/// <param name="var">Variable.</param>
	public bool checkComboStack(string[] var)
	{
		if (this.equalsComboArray(var))
		{
			this.clearComboStack();
			return true;
		}
		else
		{
			return false;
		}
		
	}
	/// <summary>
	/// Clears the combo stack.
	/// </summary>
	public void clearComboStack()
	{
		this.frameMoveStack = 0;
		this.counterMoveStack = 0;
		this.comboStack = new string[10]{null,null,null,null,null,null,null,null,null,null};
	}

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
		if (getFrameMoveStack() == getMaxFrameMoveStack()) 
		{
			clearComboStack();
		}

	}
}
