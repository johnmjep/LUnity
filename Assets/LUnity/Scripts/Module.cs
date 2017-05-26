using System;
using System.Collections;

namespace LUnity
{
    /// <summary>
    /// Clase to hold a single L-System Module
    /// </summary>
    public class Module : IEnumerable, ICloneable
    {
        #region Fields
        public string Name { get; private set; }
        public float[] Parameters;

        public float this[int i]
        {
            get { return Parameters[i]; }
            set { Parameters[i] = value; }
        }
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Module name</param>
        public Module(string name)
            : this(name, null) { }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="module">Module to copy</param>
        public Module(Module module)
            : this(module.Name, module.Parameters)
        { }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Module name</param>
        /// <param name="parameters">Module Parameters</param>
        public Module(string name, float[] parameters)
        {
            Name = name;
            Parameters = parameters;
        }
        #endregion Constructors

        #region Methods
        /// <summary>
        /// Returns Enumerator for parameters array
        /// </summary>
        /// <returns>Enumerator for parameter array</returns>
        public IEnumerator GetEnumerator()
        {
            return Parameters.GetEnumerator();
        }

        /// <summary>
        /// Overrides base ToString method
        /// </summary>
        /// <returns>String representation of object</returns>
        public override string ToString()
        {
            string retStr = string.Format("$$ Module | Name: {0}, Parameters: ", Name);
            if (Parameters != null)
            {
                foreach (double d in Parameters)
                {
                    retStr += string.Format("{0:0.00} ", d);
                }
            }
            return retStr;
        }

        /// <summary>
        /// Sets the parameters of the module
        /// </summary>
        /// <param name="parameters">Module parameters</param>
        /// <returns>This Module</returns>
        public Module SetParameters(float[] parameters)
        {
            Parameters = parameters;
            return this;
        }     

        /// <summary>
        /// Returns true if Module is a query
        /// </summary>
        /// <returns>True if Module is a query</returns>
        public bool IsQuery()
        {
            return (Name[0] == '?');
        }

        /// <summary>
        /// Returns a new clone of this Module
        /// </summary>
        /// <returns>Clone of this Module</returns>
        public object Clone()
        {
            return new Module(this);
        }
        #endregion Methods
    }
}
