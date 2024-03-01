    public abstract class BaseManager : MonoBehaviour
    {
        protected virtual void Awake()
        {
            var type = this.GetDependencyInjectionType();

            var instance = (Object)DependencyInjection.GetInstance(type);
            if (instance != null)
            {
                DependencyInjection.RemoveInstance(type);
                GameObject.DestroyImmediate(instance);
            }

            if (this.GetDontDestroyOnLoad())
                GameObject.DontDestroyOnLoad(this.gameObject);

            DependencyInjection.AddInstance(this);
        }

        protected virtual void OnDestroy()
        {
            DependencyInjection.RemoveInstance(this.GetType());
        }

        protected virtual System.Type GetDependencyInjectionType() => this.GetType();

        protected virtual bool GetDontDestroyOnLoad() => true;
    }
