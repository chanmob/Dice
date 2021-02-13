using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynergyManager : Singleton<SynergyManager>
{
    private Dictionary<string, int> _redSynergy;
    private Dictionary<string, int> _blueSynergy;
    private Dictionary<string, int> _greenSynergy;
    private Dictionary<string, int> _purpleSynergy;
    private Dictionary<string, int> _yellowSynergy;

    protected override void OnAwake()
    {
        base.OnAwake();

        _redSynergy = new Dictionary<string, int>();
        _blueSynergy = new Dictionary<string, int>();
        _greenSynergy = new Dictionary<string, int>();
        _purpleSynergy = new Dictionary<string, int>();
        _yellowSynergy = new Dictionary<string, int>();
    }

    public int GetSynergyCount(DiceType type)
    {
        switch (type)
        {
            case DiceType.Red:
                return _redSynergy.Count;
            case DiceType.Blue:
                return _blueSynergy.Count;
            case DiceType.Green:
                return _greenSynergy.Count;
            case DiceType.Purple:
                return _purpleSynergy.Count;
            case DiceType.Yellow:
                return _yellowSynergy.Count;
        }

        return 0;
    }

    public int GetRedSynergyCount(string key)
    {
        if (_redSynergy.ContainsKey(key))
        {
            return _redSynergy[key];
        }

        return 0;
    }

    public int GetBlueSynergyCount(string key)
    {
        if (_blueSynergy.ContainsKey(key))
        {
            return _blueSynergy[key];
        }

        return 0;
    }

    public int GetGreenSynergyCount(string key)
    {
        if (_greenSynergy.ContainsKey(key))
        {
            return _greenSynergy[key];
        }

        return 0;
    }

    public int GetPurpleSynergyCount(string key)
    {
        if (_purpleSynergy.ContainsKey(key))
        {
            return _purpleSynergy[key];
        }

        return 0;
    }

    public int GetYellowSynergyCount(string key)
    {
        if (_yellowSynergy.ContainsKey(key))
        {
            return _yellowSynergy[key];
        }

        return 0;
    }

    public void AddSynergy(DiceType type, string key)
    {
        switch (type)
        {
            case DiceType.Red:
                if (_redSynergy.ContainsKey(key))
                    _redSynergy[key]++;
                else
                    _redSynergy.Add(key, 1);
                break;
            case DiceType.Blue:
                if (_blueSynergy.ContainsKey(key))
                    _blueSynergy[key]++;
                else
                    _blueSynergy.Add(key, 1);
                break;
            case DiceType.Green:
                if (_greenSynergy.ContainsKey(key))
                    _greenSynergy[key]++;
                else
                    _greenSynergy.Add(key, 1);
                break;
            case DiceType.Purple:
                if (_purpleSynergy.ContainsKey(key))
                    _purpleSynergy[key]++;
                else
                    _purpleSynergy.Add(key, 1);
                break;
            case DiceType.Yellow:
                if (_yellowSynergy.ContainsKey(key))
                    _yellowSynergy[key]++;
                else
                    _yellowSynergy.Add(key, 1);
                break;
        }
    }

    public void RemoveSynergy(DiceType type, string key)
    {
        switch (type)
        {
            case DiceType.Red:
                if (_redSynergy.ContainsKey(key))
                {
                    _redSynergy[key]--;
                    if (_redSynergy[key] == 0)
                        _redSynergy.Remove(key);
                }
                break;
            case DiceType.Blue:
                if (_blueSynergy.ContainsKey(key))
                {
                    _blueSynergy[key]--;
                    if (_blueSynergy[key] == 0)
                        _blueSynergy.Remove(key);
                }
                break;
            case DiceType.Green:
                if (_greenSynergy.ContainsKey(key))
                {
                    _greenSynergy[key]--;
                    if (_greenSynergy[key] == 0)
                        _greenSynergy.Remove(key);
                }
                break;
            case DiceType.Purple:
                if (_purpleSynergy.ContainsKey(key))
                {
                    _purpleSynergy[key]--;
                    if (_purpleSynergy[key] == 0)
                        _purpleSynergy.Remove(key);
                }
                break;
            case DiceType.Yellow:
                if (_yellowSynergy.ContainsKey(key))
                {
                    _yellowSynergy[key]--;
                    if (_yellowSynergy[key] == 0)
                        _yellowSynergy.Remove(key);
                }
                break;
        }
    }
}