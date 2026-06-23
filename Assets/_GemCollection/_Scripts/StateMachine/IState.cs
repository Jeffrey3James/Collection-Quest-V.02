public interface IState 
{
    string StateName { get;}
    
    void OnEnter();
    void Update();
    void FixedUpdate();
    void OnExit();

}
