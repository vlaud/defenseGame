using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Subject
{
    public void AddObserver(Observer ops);
    public void RemoveObserver(Observer ops);
    public void NotifyToObserver();
}

public interface Observer
{
    public void Notified();
    public void SetMySubject(Subject s);
}
public class DeckManager : MonoBehaviour, Subject
{
    private Dictionary<int, List<Observer>> unitTypeListMap = new Dictionary<int, List<Observer>>();
    public List<Observer> observers = new List<Observer>();
    // Start is called before the first frame update
    void Start()
    {
        Observer[] decks;
        decks = FindObjectsOfType(typeof(UnitDeck)) as UnitDeck[];
        foreach (Observer deck in decks)
        {
            AddObserver(deck);
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            NotifyToObserver();
        }
    }
    public void AddObserver(Observer ops)
    {
        ops.SetMySubject(this);
        observers.Add(ops);
    }
    public void RemoveObserver(Observer ops)
    {
        if (observers.IndexOf(ops) > 0) { observers.Remove(ops); }
    }
    public void NotifyToObserver()
    {
        foreach (Observer ops in observers) { ops.Notified(); }
    }
}
