namespace Orbifold.Numerics
{
    /// <summary>
    /// Enumerates the supported prefix multiplier.
    /// </summary>
    public enum MultiplierPrefix
    {
        /// <summary>
        /// No multiplier.
        /// </summary>
        None,
        /// <summary>
        /// Equals: 1E-18; Prefix: "a"
        /// </summary>
        Atto,
        /// <summary>
        /// Equals: 0.01; Prefix: "c"
        /// </summary>
        Centi,
        /// <summary>
        /// Equals: 0.1; Prefix: "d"
        /// </summary>
        Deci,
        /// <summary>
        /// Equals: 10; Prefix: "da" or "e"
        /// </summary>
        Dekao,
        /// <summary>
        /// Equals: 1E+18; Prefix: "E"
        /// </summary>
        Exa,
        /// <summary>
        /// Equals: 1E-15; Prefix: "f"
        /// </summary>
        Femto,
        /// <summary>
        /// Equals: 1E+09; Prefix: "G"
        /// </summary>
        Giga,
        /// <summary>
        /// Equals: 100; Prefix: "h"
        /// </summary>
        Hecto,
        /// <summary>
        /// Equals: 1000; Prefix: "k"
        /// </summary>
        Kilo,
        /// <summary>
        /// Equals: 1E+06; Prefix: "M"
        /// </summary>
        Mega,
        /// <summary>
        /// Equals: 1E-06; Prefix: "u"
        /// </summary>
        Micro,
        /// <summary>
        /// Equals: 1E-03; Prefix: "m"
        /// </summary>
        Milli,
        /// <summary>
        /// Equals: 1E-09; Prefix: "n"
        /// </summary>
        Nano,
        /// <summary>
        /// Equals: 1E+15; Prefix: "P"
        /// </summary>
        Peta,
        /// <summary>
        /// Equals: 1E-12; Prefix: "p"
        /// </summary>
        Pico,
        /// <summary>
        /// Equals: 1E+12; Prefix: "T"
        /// </summary>
        Tera,
        /// <summary>
        /// Equals: 1E-24; Prefix: "y"
        /// </summary>
        Yocto,
        /// <summary>
        /// Equals: 1E+24; Prefix: "Y"
        /// </summary>
        Yotta,
        /// <summary>
        /// Equals: 1E-21; Prefix: "z"
        /// </summary>
        Zepto,
        /// <summary>
        /// Equals: 1E+21; Prefix: "Z"
        /// </summary>
        Zetta,
        /// <summary>
        /// Equals: 2^10 =1024; Prefix: "ki"
        /// </summary>
        Kibi,
        /// <summary>
        /// Equals: 2^20 = 1048576; Prefix: "Mi"
        /// </summary>
        Mebi,
        /// <summary>
        /// Equals: 2^30 = 1073741824; Prefix: "Gi" 
        /// </summary>
        Gibi,
        /// <summary>
        /// Equals: 2^40 = 1099511627776; Prefix: ""Ti" 
        /// </summary>
        Tebi,
        /// <summary>
        /// Equals: 2^50 = 1125899906842624; Prefix: ""Pi" 
        /// </summary>
        Pebi,
        /// <summary>
        /// Equals: 2^60 = 1152921504606846976; Prefix: "Ei"
        /// </summary>
        Exbi,
        /// <summary>
        /// Equals: 2^70 = 1180591620717411303424; Prefix: "Zi" 
        /// </summary>
        Zebi,
        /// <summary>
        /// Equals: 2^80 = 1208925819614629174706176; Prefix: "Yi" 
        /// </summary>
        Yobi
    }
}