using System;

namespace PowerTest
{
    public abstract class Test: ITestReference
    {
        public event EventHandler<Test> OnBeforeLoad;
        public event EventHandler<Test> OnAfterLoad;
        public event EventHandler<Test> OnBeforeUnload;
        public event EventHandler<Test> OnAfterUnload;
        public event EventHandler<Test> OnBeforeSetup;
        public event EventHandler<Test> OnAfterSetup;
        public event EventHandler<Test> OnBeforeExecute;
        public event EventHandler<Test> OnAfterExecute;
        public event EventHandler<Test> OnBeforeCleanup;
        public event EventHandler<Test> OnAfterCleanup;

        public event EventHandler<Test> OnBeforeRuntimeMemoryChange;
        public event EventHandler<Test> OnAfterRuntimeMemoryChange;

        public readonly Guid Id;
        public readonly bool reuseable;
        public readonly TestRunReference[] Dependencies;

        public RuntimeMemoryOutputs _generalOutputs;
        public RuntimeMemory _inputs;
        public RuntimeMemory _outputs;
        public RuntimeMemory _globalMemory;
        public IOutputInterface _log;

        public RuntimeMemoryOutputs GeneralOutputs { get => this._generalOutputs ?? throw new RuntimeMemoryNotDefinedException();}
        public RuntimeMemory Inputs { get => this._inputs ?? throw new RuntimeMemoryNotDefinedException(); }
        public RuntimeMemory Outputs { get => this._outputs ?? throw new RuntimeMemoryNotDefinedException(); }
        public RuntimeMemory GlobalMemory { get => this._globalMemory ?? throw new RuntimeMemoryNotDefinedException(); }
        public IOutputInterface Log { get => this._log ?? throw new RuntimeMemoryNotDefinedException(); }

        public Test(Guid id, TestRunReference[] Dependencies, bool reuseable)
        {
            this.Id = id;
            this.Dependencies = Dependencies;
            this.reuseable = reuseable;
        }
        public Test(Guid id, TestRunReference[] Dependencies):this(id, Dependencies, true){}
        public Test(TestRunReference [] Dependencies) : this(Guid.NewGuid(), Dependencies) { }
        public Test() : this(new TestRunReference[] { }) { }

        public bool IsRuntimeMemoryDefined() => this._globalMemory != null;
        private void SetRuntimeMemory(TestEngine engine, RuntimeMemory inputs, RuntimeMemory outputs, RuntimeMemoryOutputs allOutputs, RuntimeMemory global, IOutputInterface log, bool raiseEvent)
        {
            if (raiseEvent) this.OnBeforeRuntimeMemoryChange?.Invoke(engine, this);
            this._inputs = inputs;
            this._outputs = outputs;
            this._generalOutputs = allOutputs;
            this._globalMemory = global;
            this._log = log;
            if (raiseEvent) this.OnAfterRuntimeMemoryChange?.Invoke(engine, this);
        }
        internal void internalLoad(TestEngine engine)
        {
            this.OnBeforeLoad?.Invoke(engine, this);
            this.Setup();
            this.OnAfterLoad?.Invoke(engine, this);
        }
        internal void internalUnLoad(TestEngine engine)
        {
            this.OnBeforeUnload?.Invoke(engine, this);
            this.Setup();
            this.OnAfterUnload?.Invoke(engine, this);
        }
        internal void internalSetup(TestEngine engine, RuntimeMemory inputs, RuntimeMemory outputs, RuntimeMemoryOutputs allOutputs, RuntimeMemory global, IOutputInterface log)
        {
            SetRuntimeMemory(engine, inputs, outputs, allOutputs, global, log, true);
            this.OnBeforeSetup?.Invoke(engine, this);
            this.Setup();
            this.OnAfterSetup?.Invoke(engine, this);
        }

        internal void internalCleanup(TestEngine engine)
        {
            this.OnBeforeCleanup?.Invoke(engine, this);
            this.Cleanup();
            this.OnAfterCleanup?.Invoke(engine, this);
            SetRuntimeMemory(engine, null, null, null, null,null, true);
        }
        internal void internalExecute(TestEngine engine)
        {
            this.OnBeforeExecute?.Invoke(engine, this);
            this.Execute();
            this.OnAfterExecute?.Invoke(engine, this);
        }
        protected virtual void Setup() {}
        protected virtual void Cleanup() {}
        protected virtual void Load() {}
        protected virtual void Unload() {}
        public abstract void Execute();
        public Guid GetId() => this.Id;
    }
}
