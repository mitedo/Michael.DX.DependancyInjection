using DevExpress.Xpo.Metadata;
using Michael.Xpo;
using System.ComponentModel;

namespace DevExpress.Xpo
{
    [NonPersistent]
    public class Entity : XPObject
    {
        [NonPersistent]
        [MemberDesignTimeVisibility(false)]
        [Browsable(false)]
        protected bool IsInitialized { get; private set; } = false;
        [NonPersistent]
        [MemberDesignTimeVisibility(false)]
        [Browsable(false)]
        protected bool IsInitializing { get; private set; } = false;

        protected Entity(Session session) : base(session)
        {
            if (!IsInitialized && !IsInitializing)
            {
                InitializeThisObject();
            }
        }

        protected Entity() : base()
        {

            if (!IsInitialized && !IsInitializing)
            {
                InitializeThisObject();
            }
        }

        protected Entity(Session session, XPClassInfo xpclassinfo) : base(session, xpclassinfo)
        {
            if (!IsInitialized && !IsInitializing)
            {
                InitializeThisObject();
            }
        }
        public override void AfterConstruction()
        {
            if (!IsInitialized && !IsInitializing)
            {
                InitializeThisObject();
            }
            base.AfterConstruction();
        }

        protected override void OnLoading()
        {
            if (!IsInitialized && !IsInitializing)
            {
                InitializeThisObject();
            }
            base.OnLoading();
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            if (!IsInitialized && !IsInitializing)
            {
                InitializeThisObject();
            }
            base.OnChanged(propertyName, oldValue, newValue);
        }

        private void InitializeThisObject()
        {
            IsInitializing = true;
            try
            {
                HandleAttributes.Fix(this);
            }
            finally
            {
                this.IsInitialized = true;
                IsInitializing = false;
            }
        }
    }
}