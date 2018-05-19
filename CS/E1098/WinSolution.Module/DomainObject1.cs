using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using System;

namespace WinSolution.Module {
    [DefaultClassOptions]
    public class DomainObject1 : BaseObject {
        public DomainObject1(Session session) : base(session) { }
        private DateTime _CreatedOn;
        public DateTime CreatedOn {
            get { return _CreatedOn; }
            set { SetPropertyValue("CreatedOn", ref _CreatedOn, value); }
        }
        private string _Name;
        public string Name {
            get { return _Name; }
            set { SetPropertyValue("Name", ref _Name, value); }
        }
    }
}
