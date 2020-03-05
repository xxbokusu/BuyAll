///<summary>
/// <filename>ファイル名:Singleton.cs</filename>
///</summary>

public class Singleton<T> where T :class,new() {
    private static T instance;

    protected Singleton() { }

    public static T Instance {
        get {
            if(null == instance) {
                instance = new T();
            }
            return instance;
        }
    }

    public static T CreateInstance() {
        if(null == instance) {
            instance = new T(); 
        }
        return instance;
    }

    
    public static bool ExistInstance {
        get{ return null != instance;}
    }

}
