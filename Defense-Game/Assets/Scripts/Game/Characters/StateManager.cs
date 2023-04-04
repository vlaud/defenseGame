using UnityEngine;

public enum MyState
{
    Idle,
    Move,
    Attack,
}
public interface State
{
    void Action();
}
public class StateManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Character character = new Character(new Move());
        character.act();

        character.setState(new Attack());
        character.act();

        character.setState(new Move());
        character.act();
    }

}
public abstract class StateMachine : MonoBehaviour
{
    public abstract void DoAction(MyState state);
};