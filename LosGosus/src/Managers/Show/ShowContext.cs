public class ShowContext<T> 
{
    IShow<T>? show;
    public void SetShow(IShow<T> show){
        this.show = show;
    }

    public void Execute(List<T> items, string criteria){
        if(show != null)
        {
            show.ShowResult(items, criteria);
        }
    }
}