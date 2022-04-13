public interface IGameResult
{
    public void IGameWin(int _pointCount = 0);
    public void IGameOver(int _pointCount = 0);
    public void IGameNextLevel();
    public void IGameRestart();


}