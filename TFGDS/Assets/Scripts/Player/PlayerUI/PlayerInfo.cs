﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum InfoType
{
    Name,
    Level,
    Power,
    Exp,
    Coin,
    MP,
    HP,
    Stamina,
    All
}

public class PlayerInfo : MonoBehaviour
{
   private float currentRef;
    public static PlayerInfo instance_;

    public Text coinText;

    #region property

    private string name_;
    private int level_;
    private int hp_;
    private int mp_;
    private int stamina_;
    private int exp_;
    private int coin_;
    private int power_;
    private int basicStrength = 10;
    private int basicIntellect = 10;
    private int basicAgility = 10;
    private bool cursoModeState = false;
    #endregion

    #region gettter setter

    public int BasicStrength
    {
        get { return basicStrength; }
        set { basicStrength = value; }
    }

    public bool CursoModeState
    {
        get { return cursoModeState; }
        set { cursoModeState = value; }
    }

    public int BasicIntellect
    {
        get { return basicIntellect; }
        set { basicIntellect = value; }
    }

    public int BasicAgility
    {
        get { return basicAgility; }
        set { basicAgility = value; }
    }

    public string Name
    {
        get { return name_; }
        set { name_ = value; }
    }

    public int Power
    {
        get { return power_; }
        set { power_ = value; }
    }

    public int Level
    {
        get { return level_; }
        set { level_ = value; }
    }

    public int MP
    {
        get { return mp_; }
        set { mp_ = value; }
    }

    public int HP
    {
        get { return hp_; }
        set { hp_ = value; }
    }

    public int Stamina
    {
        get { return stamina_; }
        set { stamina_ = value; }
    }

    public int Exp
    {
        get { return exp_; }
        set { exp_ = value; }
    }

    public int Coin
    {
        get { return coin_; }
        set { coin_ = value; }
    }

    #endregion


    private float staminaTimer = 0;
    private float mpTimer = 0;
    private float hpTimer = 0;

    private void Awake()
    {
        instance_ = this; 
    }


    private void Start()
    {
        Init();
        coinText.text = this.Coin.ToString();
    }
    private void Update()
    {
        //CheckCeroState();
        StatChanged();
    }

    //control de eventos
    public delegate void OnPlayerInfoChangeEvent(InfoType type);
    public event OnPlayerInfoChangeEvent OnPlayerInfoChanged;
    /// <summary>
    /// Inicializacion de los info del player
    /// </summary>
    void Init()
    {
        this.HP = 100;
        this.MP = 100;
        this.Exp = 0;
        this.Coin = 1000;
        this.Stamina = 100;
        this.Level = 1;
        this.Power = 10;
        OnPlayerInfoChanged(InfoType.All);
        //Debug.Log("hp es" + this.HP);
    }


    void StatChanged()
    {
        
        //Debug.Log("stamina  " + this.Stamina + "  hp " + this.HP);
        //Aumento de stamina segun el tiempo transcurrido
        if (this.Stamina < 100)
        {
            staminaTimer += Time.deltaTime;
            if (staminaTimer > 2)
            {
                float newValue = Mathf.SmoothDamp((float)this.Stamina, 15.0f, ref currentRef, 0.3f);
                this.Stamina += (int)newValue;
                staminaTimer -= 2;
                OnPlayerInfoChanged(InfoType.Stamina);
                if (this.Stamina > 100)
                {
                    this.Stamina = 100;
                }
                else
                {
                    this.staminaTimer = 0;
                }
            }               
            //Debug.Log("sumando stamina"+Stamina);
            //Debug.Log("entrando aqui");       
            if(this.Stamina <= 0)
            {
                Stamina = 1;
            }
        }
        
        //Aumento de mp segun el tiempo transcurrido
        if (this.MP < 100)
        {
            mpTimer += Time.deltaTime;
            if (mpTimer > 10)
            {
                MP += 5;
                mpTimer -= 10f;
                OnPlayerInfoChanged(InfoType.MP);
            }
            else
            {
                this.mpTimer = 0;
            }
        }
        else if(this.MP <= 0)
        {
            this.MP = 1;
        }

        //Aumento de hp segun el tiempo transcurrido
        if (this.HP < 100)
        {
            hpTimer += Time.deltaTime;
            if (hpTimer > 10)
            {
                HP += 10;
                hpTimer -= 10f;
                OnPlayerInfoChanged(InfoType.HP);
            }
            else
            {
                this.hpTimer = 0;
            }
        }
        
    }
    /// <summary>
    /// Methodoss para gastar monedas
    /// </summary>
    public bool ConsumeCoin(int amount)
    {
        if(this.Coin >= amount)
        {
            this.Coin -= amount;
            coinText.text = this.Coin.ToString();
            OnPlayerInfoChanged(InfoType.Coin);
            return true;
        }
        return false;
    }   
    /// <summary>
    /// Mehtos para ganar monedas
    /// </summary>
    /// <param name="amount"></param>
    public void EarnCoin(int amount)
    {
        this.Coin += amount;
        coinText.text = this.Coin.ToString();
        OnPlayerInfoChanged(InfoType.Coin);
    }

    public void CosumeStamina()
    {
        this.Stamina -= 10;
        OnPlayerInfoChanged(InfoType.Stamina);
    }

    public void HealthDamageRest(int amout)
    {
        this.HP -= amout;
        if (this.HP <= 0) this.HP = 0;
        OnPlayerInfoChanged(InfoType.HP);
    }

    public void HealthAdd(int amout)
    {
        this.HP += amout;
        this.HP = Mathf.Clamp(this.HP, 0, 100);
        OnPlayerInfoChanged(InfoType.HP);
    }
}
