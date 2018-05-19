using System;
using System.ComponentModel;

using DevExpress.Xpo;

using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;

namespace AccessActionControl.Module {
    [DefaultClassOptions]
    public class MyDomainObject : BaseObject {
        public MyDomainObject(Session session) : base(session) { }

        public DateTime CreatedOn {
            get { return GetPropertyValue<DateTime>("CreatedOn"); }
            set { SetPropertyValue("CreatedOn", value); }
        }

        public string Title {
            get { return GetPropertyValue<string>("Title"); }
            set { SetPropertyValue("Title", value); }
        }
    }
}
