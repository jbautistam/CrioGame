using System;
using System.Diagnostics;

namespace Bau.Libraries.CrioGame.Common.Models.Structs
{
  /// <summary>
  ///		Color en cuatro bytes (RGBA)
  /// </summary>
  [DebuggerDisplay("{DebugDisplayString,nq}")]
  public struct ColorEngine : IEquatable<ColorEngine>
  {
		public ColorEngine(ColorEngine clrColor, float fltAlpha) : this(clrColor, (int) (fltAlpha * 255)) {}

		public ColorEngine(float r, float g, float b) : this((int) (r * 255), (int) (g * 255), (int) (b * 255)) {}

		public ColorEngine(float r, float g, float b, float alpha) : this((int) (r * 255), (int) (g * 255), (int) (b * 255), (int) (alpha * 255)) 
		{
		}

		public ColorEngine(uint intPackedValue)
		{ PackedValue = intPackedValue;
		}

		public ColorEngine(byte r, byte g, byte b, byte alpha)
		{ PackedValue = ((uint) alpha << 24) | ((uint) b << 16) | ((uint) g << 8) | (r);
		}

		public ColorEngine(ColorEngine clrColor, int intAlpha)
		{	if ((intAlpha & 0xFFFFFF00) != 0)
				{ uint clampedA = (uint) Clamp(intAlpha, Byte.MinValue, Byte.MaxValue);

						PackedValue = (clrColor.PackedValue & 0x00FFFFFF) | (clampedA << 24);
				}
			else
				PackedValue = (clrColor.PackedValue & 0x00FFFFFF) | ((uint) intAlpha << 24);
		}

		public ColorEngine(int r, int g, int b)
		{	// Inicializa el valor
				PackedValue = 0xFF000000; // A = 255
			// Calcula el valor
				if (((r | g | b) & 0xFFFFFF00) != 0)
					{	uint clampedR = (uint) Clamp(r, byte.MinValue, byte.MaxValue);
						uint clampedG = (uint) Clamp(g, byte.MinValue, byte.MaxValue);
						uint clampedB = (uint) Clamp(b, byte.MinValue, byte.MaxValue);

							PackedValue |= (clampedB << 16) | (clampedG << 8) | (clampedR);
					}
				else
					PackedValue |= ((uint) b << 16) | ((uint) g << 8) | ((uint) r);
		}

		public ColorEngine(int r, int g, int b, int alpha)
		{	if (((r | g | b | alpha) & 0xFFFFFF00) != 0)
				{	uint clampedR = (uint) Clamp(r, byte.MinValue, byte.MaxValue);
					uint clampedG = (uint) Clamp(g, byte.MinValue, byte.MaxValue);
					uint clampedB = (uint) Clamp(b, byte.MinValue, byte.MaxValue);
					uint clampedA = (uint) Clamp(alpha, byte.MinValue, byte.MaxValue);

						PackedValue = (clampedA << 24) | (clampedB << 16) | (clampedG << 8) | (clampedR);
				}
			else
				PackedValue = ((uint) alpha << 24) | ((uint) b << 16) | ((uint) g << 8) | ((uint) r);
		}

		/// <summary>
		///		Obtiene un valor entre dos límites
		/// </summary>
		private static int Clamp(int intValue, int  intMinimum, int intMaximum)
		{ if (intValue < intMinimum)
				return intMinimum;
			else if (intValue > intMaximum)
				return intMaximum;
			else
				return intValue;
		}

		///// <summary>
		/////		Interpolación linear entre <see cref="Color"/>
		///// </summary>
		//public static Color Lerp(Color value1, Color value2, Single amount)
		//{ amount = Clamp(amount, 0, 1);
		//		return new Color(   
		//				(int) Lerp(value1.R, value2.R, amount),
		//				(int) Lerp(value1.G, value2.G, amount),
		//				(int) Lerp(value1.B, value2.B, amount),
		//				(int) Lerp(value1.A, value2.A, amount) );
		//}

		/// <summary>
		///		Hashcode de <see cref="Color"/>
		/// </summary>
		public override int GetHashCode()
		{	return PackedValue.GetHashCode();
		}
	
		/// <summary>
		///		Compara si una instancia es igual que un ojeto
		/// </summary>
		public override bool Equals(object obj)
		{ return (obj is ColorEngine) && Equals((ColorEngine) obj);
		}
		
		/// <summary>
    ///		Compara si la instancia actual es igual a <see cref="ColorEngine"/>
    /// </summary>
    public bool Equals(ColorEngine clrOther)
    { return PackedValue == clrOther.PackedValue;
    }

		/// <summary>
		/// Compara si dos instancias de <see cref="ColorEngine"/> son iguales
		/// </summary>
		public static bool operator ==(ColorEngine a, ColorEngine b)
		{ return a.PackedValue == b.PackedValue;
		}
	
		/// <summary>
		///		Compara si dos instancias de <see cref="ColorEngine"/> son distintas
		/// </summary>
		public static bool operator !=(ColorEngine a, ColorEngine b)
		{ return a.PackedValue != b.PackedValue;
		}

		/// <summary>
		///		Multiplica el <see cref="ColorEngine"/> por un valor escalar
		/// </summary>
		public static ColorEngine operator *(ColorEngine clrValue, float fltScale)
		{ return new ColorEngine((int) (clrValue.R * fltScale), (int) (clrValue.G * fltScale), (int) (clrValue.B * fltScale), (int) (clrValue.A * fltScale));
		}

		/// <summary>
		///		Valor del color almacenado como RGBA (R es el octeto menos significativo)
		/// </summary>
		public uint PackedValue { get; private set; }

		/// <summary>
		///		Cadena para mostrar la información de depuración del color
		/// </summary>
    internal string DebugDisplayString
    {	get { return ToString(); }
    }

    /// <summary>
    ///		Cadena con el color representado en formato {R: xx G: xx B: xx A: xx}
    /// </summary>
		public override string ToString ()
		{ return $"{{R: {R} G: {G} B: {B} A: {A}}}";
		}
	
		/// <summary>
    ///		Traduce un color con la información no premultiplicada en un color que contiene la información de transparencia premultiplicada
    /// </summary>
    public static ColorEngine FromNonPremultiplied(int intRed, int intGreen, int intBlue, int intAlpha)
    { return new ColorEngine(intRed * intAlpha / 255, intGreen * intAlpha / 255, intBlue * intAlpha / 255, intAlpha);
    }

		/// <summary>
		///		Componente Azul
		/// </summary>
		public byte B
		{ get 
				{ unchecked
						{	return (byte) (PackedValue >> 16);
						}
				}
			set { PackedValue = (PackedValue & 0xff00ffff) | ((uint) value << 16); }
		}

		/// <summary>
		///		Componente Verde
		/// </summary>
		public byte G
		{ get
				{	unchecked
						{	return (byte) (PackedValue >> 8);
						}
				}
			set { PackedValue = (PackedValue & 0xffff00ff) | ((uint) value << 8); }
		}

		/// <summary>
		///		Componente rojo
		/// </summary>
		public byte R
		{	get
				{	unchecked
						{	return (byte) PackedValue;
						}
				}
			set { PackedValue = (PackedValue & 0xffffff00) | value; }
		}

		/// <summary>
		///		Componente alfa (transparencia)
		/// </summary>
		public byte A
		{	get
				{	unchecked
						{ return (byte) (PackedValue >> 24);
						}
				}
			set { PackedValue = (PackedValue & 0x00ffffff) | ((uint) value << 24); }
		}

		/// <summary> Color TransparentBlack (R:0,G:0,B:0,A:0) </summary>
		public static ColorEngine TransparentBlack { get; } = new ColorEngine(0);
        
		/// <summary> Color Transparent (R:0,G:0,B:0,A:0) </summary>
		public static ColorEngine Transparent { get; } = new ColorEngine(0);
	
		/// <summary> Color AliceBlue (R:240,G:248,B:255,A:255) </summary>
		public static ColorEngine AliceBlue { get; } = new ColorEngine(0xfffff8f0);
        
		/// <summary> Color AntiqueWhite (R:250,G:235,B:215,A:255) </summary>
		public static ColorEngine AntiqueWhite { get; } = new ColorEngine(0xffd7ebfa);
        
		/// <summary> Color Aqua (R:0,G:255,B:255,A:255) </summary>
		public static ColorEngine Aqua { get; } = new ColorEngine(0xffffff00);
	
		/// <summary> Color Aquamarine (R:127,G:255,B:212,A:255) </summary>
		public static ColorEngine Aquamarine { get; } = new ColorEngine(0xffd4ff7f);
        
		/// <summary> Color Azure (R:240,G:255,B:255,A:255) </summary>
		public static ColorEngine Azure { get; } = new ColorEngine(0xfffffff0);
	
		/// <summary> Color Beige (R:245,G:245,B:220,A:255) </summary>
		public static ColorEngine Beige { get; } = new ColorEngine(0xffdcf5f5);
        
		/// <summary> Color Bisque (R:255,G:228,B:196,A:255) </summary>
		public static ColorEngine Bisque { get; } = new ColorEngine(0xffc4e4ff);
        
		/// <summary> Color Black (R:0,G:0,B:0,A:255) </summary>
		public static ColorEngine Black { get; } = new ColorEngine(0xff000000);
        
		/// <summary> Color BlanchedAlmond (R:255,G:235,B:205,A:255) </summary>
		public static ColorEngine BlanchedAlmond { get; } = new ColorEngine(0xffcdebff);
        
		/// <summary> Color Blue (R:0,G:0,B:255,A:255) </summary>
		public static ColorEngine Blue { get; } = new ColorEngine(0xffff0000);
        
		/// <summary> Color BlueViolet (R:138,G:43,B:226,A:255) </summary>
		public static ColorEngine BlueViolet { get; } = new ColorEngine(0xffe22b8a);
        
		/// <summary> Color Brown (R:165,G:42,B:42,A:255) </summary>
		public static ColorEngine Brown { get; } = new ColorEngine(0xff2a2aa5);
        
		/// <summary> Color BurlyWood (R:222,G:184,B:135,A:255) </summary>
		public static ColorEngine BurlyWood { get; } = new ColorEngine(0xff87b8de);
        
		/// <summary> Color CadetBlue (R:95,G:158,B:160,A:255) </summary>
		public static ColorEngine CadetBlue { get; } = new ColorEngine(0xffa09e5f);
        
		/// <summary> Color Chartreuse (R:127,G:255,B:0,A:255) </summary>
		public static ColorEngine Chartreuse { get; } = new ColorEngine(0xff00ff7f);
         
		/// <summary> Color Chocolate (R:210,G:105,B:30,A:255) </summary>
		public static ColorEngine Chocolate { get; } = new ColorEngine(0xff1e69d2);
        
		/// <summary> Color Coral (R:255,G:127,B:80,A:255) </summary>
		public static ColorEngine Coral { get; } = new ColorEngine(0xff507fff);

		/// <summary> Color CornflowerBlue (R:100,G:149,B:237,A:255) </summary>
		public static ColorEngine CornflowerBlue { get; } = new ColorEngine(0xffed9564);
        
		/// <summary> Color Cornsilk (R:255,G:248,B:220,A:255) </summary>
		public static ColorEngine Cornsilk { get; } = new ColorEngine(0xffdcf8ff);
	
		/// <summary> Color Crimson (R:220,G:20,B:60,A:255) </summary>
		public static ColorEngine Crimson { get; } = new ColorEngine(0xff3c14dc);
        
		/// <summary> Color Cyan (R:0,G:255,B:255,A:255) </summary>
		public static ColorEngine Cyan { get; } = new ColorEngine(0xffffff00);
        
		/// <summary> Color DarkBlue (R:0,G:0,B:139,A:255) </summary>
		public static ColorEngine DarkBlue { get; } = new ColorEngine(0xff8b0000);
	
		/// <summary> Color DarkCyan (R:0,G:139,B:139,A:255) </summary>
		public static ColorEngine DarkCyan { get; } = new ColorEngine(0xff8b8b00);
        
		/// <summary> Color DarkGoldenrod (R:184,G:134,B:11,A:255) </summary>
		public static ColorEngine DarkGoldenrod { get; } = new ColorEngine(0xff0b86b8);
        
		/// <summary> Color DarkGray (R:169,G:169,B:169,A:255) </summary>
		public static ColorEngine DarkGray { get; } = new ColorEngine(0xffa9a9a9);
	
		/// <summary> Color DarkGreen (R:0,G:100,B:0,A:255) </summary>
		public static ColorEngine DarkGreen { get; } = new ColorEngine(0xff006400);
        
		/// <summary> Color DarkKhaki (R:189,G:183,B:107,A:255) </summary>
		public static ColorEngine DarkKhaki { get; } = new ColorEngine(0xff6bb7bd);

		/// <summary> Color DarkMagenta (R:139,G:0,B:139,A:255) </summary>
		public static ColorEngine DarkMagenta { get; } = new ColorEngine(0xff8b008b);

		/// <summary> Color DarkOliveGreen (R:85,G:107,B:47,A:255) </summary>
		public static ColorEngine DarkOliveGreen { get; } = new ColorEngine(0xff2f6b55);

		/// <summary> Color DarkOrange (R:255,G:140,B:0,A:255) </summary>
		public static ColorEngine DarkOrange { get; } = new ColorEngine(0xff008cff);

		/// <summary> Color DarkOrchid (R:153,G:50,B:204,A:255) </summary>
		public static ColorEngine DarkOrchid { get; } = new ColorEngine(0xffcc3299);

		/// <summary> Color DarkRed (R:139,G:0,B:0,A:255) </summary>
		public static ColorEngine DarkRed { get; } = new ColorEngine(0xff00008b);
        
		/// <summary> Color DarkSalmon (R:233,G:150,B:122,A:255) </summary>
		public static ColorEngine DarkSalmon { get; } = new ColorEngine(0xff7a96e9);

		/// <summary> Color DarkSeaGreen (R:143,G:188,B:139,A:255) </summary>
		public static ColorEngine DarkSeaGreen { get; } = new ColorEngine(0xff8bbc8f);

		/// <summary> Color DarkSlateBlue (R:72,G:61,B:139,A:255) </summary>
		public static ColorEngine DarkSlateBlue { get; } = new ColorEngine(0xff8b3d48);

		/// <summary> Color DarkSlateGray (R:47,G:79,B:79,A:255) </summary>
		public static ColorEngine DarkSlateGray { get; } = new ColorEngine(0xff4f4f2f);

		/// <summary> Color DarkTurquoise (R:0,G:206,B:209,A:255) </summary>
		public static ColorEngine DarkTurquoise { get; } = new ColorEngine(0xffd1ce00);

		/// <summary> Color DarkViolet (R:148,G:0,B:211,A:255) </summary>
		public static ColorEngine DarkViolet { get; } = new ColorEngine(0xffd30094);
         
		/// <summary> Color DeepPink (R:255,G:20,B:147,A:255) </summary>
		public static ColorEngine DeepPink { get; } = new ColorEngine(0xff9314ff);

		/// <summary> Color DeepSkyBlue (R:0,G:191,B:255,A:255) </summary>
		public static ColorEngine DeepSkyBlue { get; } = new ColorEngine(0xffffbf00);

		/// <summary> Color DimGray (R:105,G:105,B:105,A:255) </summary>
		public static ColorEngine DimGray { get; } = new ColorEngine(0xff696969);

		/// <summary> Color DodgerBlue (R:30,G:144,B:255,A:255) </summary>
		public static ColorEngine DodgerBlue { get; } = new ColorEngine(0xffff901e);

		/// <summary> Color Firebrick (R:178,G:34,B:34,A:255) </summary>
		public static ColorEngine Firebrick { get; } = new ColorEngine(0xff2222b2);

		/// <summary> Color FloralWhite (R:255,G:250,B:240,A:255) </summary>
		public static ColorEngine FloralWhite { get; } = new ColorEngine(0xfff0faff);

		/// <summary> Color ForestGreen (R:34,G:139,B:34,A:255) </summary>
		public static ColorEngine ForestGreen { get; } = new ColorEngine(0xff228b22);
        
		/// <summary> Color Fuchsia (R:255,G:0,B:255,A:255) </summary>
		public static ColorEngine Fuchsia { get; } = new ColorEngine(0xffff00ff);

		/// <summary> Color Gainsboro (R:220,G:220,B:220,A:255) </summary>
		public static ColorEngine Gainsboro { get; } = new ColorEngine(0xffdcdcdc);

		/// <summary> Color GhostWhite (R:248,G:248,B:255,A:255) </summary>
		public static ColorEngine GhostWhite { get; } = new ColorEngine(0xfffff8f8);

		/// <summary> Color Gold (R:255,G:215,B:0,A:255) </summary>
		public static ColorEngine Gold { get; } = new ColorEngine(0xff00d7ff);

		/// <summary> Color Goldenrod (R:218,G:165,B:32,A:255) </summary>
		public static ColorEngine Goldenrod { get; } = new ColorEngine(0xff20a5da);

		/// <summary> Color Gray (R:128,G:128,B:128,A:255) </summary>
		public static ColorEngine Gray { get; } = new ColorEngine(0xff808080);

		/// <summary> Color Green (R:0,G:128,B:0,A:255) </summary>
		public static ColorEngine Green { get; } = new ColorEngine(0xff008000);

		/// <summary> Color GreenYellow (R:173,G:255,B:47,A:255) </summary>
		public static ColorEngine GreenYellow { get; } = new ColorEngine(0xff2fffad);

		/// <summary> Color Honeydew (R:240,G:255,B:240,A:255) </summary>
		public static ColorEngine Honeydew { get; } = new ColorEngine(0xfff0fff0);

		/// <summary> Color HotPink (R:255,G:105,B:180,A:255) </summary>
		public static ColorEngine HotPink { get; } = new ColorEngine(0xffb469ff);
        
		/// <summary> Color IndianRed (R:205,G:92,B:92,A:255) </summary>
		public static ColorEngine IndianRed { get; } = new ColorEngine(0xff5c5ccd);
        
		/// <summary> Color Indigo (R:75,G:0,B:130,A:255) </summary>
		public static ColorEngine Indigo { get; } = new ColorEngine(0xff82004b);
        
		/// <summary> Color Ivory (R:255,G:255,B:240,A:255) </summary>
		public static ColorEngine Ivory { get; } = new ColorEngine(0xfff0ffff);
        
		/// <summary> Color Khaki (R:240,G:230,B:140,A:255) </summary>
		public static ColorEngine Khaki { get; } = new ColorEngine(0xff8ce6f0);
        
		/// <summary> Color Lavender (R:230,G:230,B:250,A:255) </summary>
		public static ColorEngine Lavender { get; } = new ColorEngine(0xfffae6e6);

		/// <summary> Color LavenderBlush (R:255,G:240,B:245,A:255) </summary>
		public static ColorEngine LavenderBlush { get; } = new ColorEngine(0xfff5f0ff);
        
		/// <summary> Color LawnGreen (R:124,G:252,B:0,A:255) </summary>
		public static ColorEngine LawnGreen { get; } = new ColorEngine(0xff00fc7c);

		/// <summary> Color LemonChiffon (R:255,G:250,B:205,A:255) </summary>
		public static ColorEngine LemonChiffon { get; } = new ColorEngine(0xffcdfaff);

		/// <summary> Color LightBlue (R:173,G:216,B:230,A:255) </summary>
		public static ColorEngine LightBlue { get; } = new ColorEngine(0xffe6d8ad);

		/// <summary> Color LightCoral (R:240,G:128,B:128,A:255) </summary>
		public static ColorEngine LightCoral { get; } = new ColorEngine(0xff8080f0);
        
		/// <summary> Color LightCyan (R:224,G:255,B:255,A:255) </summary>
		public static ColorEngine LightCyan { get; } = new ColorEngine(0xffffffe0);

		/// <summary> Color LightGoldenrodYellow (R:250,G:250,B:210,A:255) </summary>
		public static ColorEngine LightGoldenrodYellow { get; } = new ColorEngine(0xffd2fafa);
        
		/// <summary> Color LightGray (R:211,G:211,B:211,A:255) </summary>
		public static ColorEngine LightGray { get; } = new ColorEngine(0xffd3d3d3);

		/// <summary> Color LightGreen (R:144,G:238,B:144,A:255) </summary>
		public static ColorEngine LightGreen { get; } = new ColorEngine(0xff90ee90);

		/// <summary> Color LightPink (R:255,G:182,B:193,A:255) </summary>
		public static ColorEngine LightPink { get; } = new ColorEngine(0xffc1b6ff);

		/// <summary> Color LightSalmon (R:255,G:160,B:122,A:255) </summary>
		public static ColorEngine LightSalmon { get; } = new ColorEngine(0xff7aa0ff);

		/// <summary> Color LightSeaGreen (R:32,G:178,B:170,A:255) </summary>
		public static ColorEngine LightSeaGreen { get; } = new ColorEngine(0xffaab220);

		/// <summary> Color LightSkyBlue (R:135,G:206,B:250,A:255) </summary>
		public static ColorEngine LightSkyBlue { get; } = new ColorEngine(0xffface87);

		/// <summary> Color LightSlateGray (R:119,G:136,B:153,A:255) </summary>
		public static ColorEngine LightSlateGray { get; } = new ColorEngine(0xff998877);

		/// <summary> Color LightSteelBlue (R:176,G:196,B:222,A:255) </summary>
		public static ColorEngine LightSteelBlue { get; } = new ColorEngine(0xffdec4b0);

		/// <summary> Color LightYellow (R:255,G:255,B:224,A:255) </summary>
		public static ColorEngine LightYellow { get; } = new ColorEngine(0xffe0ffff);

		/// <summary> Color Lime (R:0,G:255,B:0,A:255) </summary>
		public static ColorEngine Lime { get; } = new ColorEngine(0xff00ff00);

		/// <summary> Color LimeGreen (R:50,G:205,B:50,A:255) </summary>
		public static ColorEngine LimeGreen { get; } = new ColorEngine(0xff32cd32);

		/// <summary> Color Linen (R:250,G:240,B:230,A:255) </summary>
		public static ColorEngine Linen { get; } = new ColorEngine(0xffe6f0fa);

		/// <summary> Color Magenta (R:255,G:0,B:255,A:255) </summary>
		public static ColorEngine Magenta { get; } = new ColorEngine(0xffff00ff);

		/// <summary> Color Maroon (R:128,G:0,B:0,A:255) </summary>
		public static ColorEngine Maroon { get; } = new ColorEngine(0xff000080);

		/// <summary> Color MediumAquamarine (R:102,G:205,B:170,A:255) </summary>
		public static ColorEngine MediumAquamarine { get; } = new ColorEngine(0xffaacd66);

		/// <summary> Color MediumBlue (R:0,G:0,B:205,A:255) </summary>
		public static ColorEngine MediumBlue { get; } = new ColorEngine(0xffcd0000);

		/// <summary> Color MediumOrchid (R:186,G:85,B:211,A:255) </summary>
		public static ColorEngine MediumOrchid { get; } = new ColorEngine(0xffd355ba);

		/// <summary> Color MediumPurple (R:147,G:112,B:219,A:255) </summary>
		public static ColorEngine MediumPurple { get; } = new ColorEngine(0xffdb7093);

		/// <summary> Color MediumSeaGreen (R:60,G:179,B:113,A:255) </summary>
		public static ColorEngine MediumSeaGreen { get; } = new ColorEngine(0xff71b33c);

		/// <summary> Color MediumSlateBlue (R:123,G:104,B:238,A:255) </summary>
		public static ColorEngine MediumSlateBlue { get; } = new ColorEngine(0xffee687b);

		/// <summary> Color MediumSpringGreen (R:0,G:250,B:154,A:255) </summary>
		public static ColorEngine MediumSpringGreen { get; } = new ColorEngine(0xff9afa00);

		/// <summary> Color MediumTurquoise (R:72,G:209,B:204,A:255) </summary>
		public static ColorEngine MediumTurquoise { get; } = new ColorEngine(0xffccd148);

		/// <summary> Color MediumVioletRed (R:199,G:21,B:133,A:255) </summary>
		public static ColorEngine MediumVioletRed { get; } = new ColorEngine(0xff8515c7);

		/// <summary> Color MidnightBlue (R:25,G:25,B:112,A:255) </summary>
		public static ColorEngine MidnightBlue { get; } = new ColorEngine(0xff701919);

		/// <summary> Color MintCream (R:245,G:255,B:250,A:255) </summary>
		public static ColorEngine MintCream { get; } = new ColorEngine(0xfffafff5);

		/// <summary> Color MistyRose (R:255,G:228,B:225,A:255) </summary>
		public static ColorEngine MistyRose { get; } = new ColorEngine(0xffe1e4ff);

		/// <summary> Color Moccasin (R:255,G:228,B:181,A:255) </summary>
		public static ColorEngine Moccasin { get; } = new ColorEngine(0xffb5e4ff);

		/// <summary> Color MonoGame orange theme (R:231,G:60,B:0,A:255) </summary>
		public static ColorEngine MonoGameOrange { get; } = new ColorEngine(0xff003ce7);

		/// <summary> Color NavajoWhite (R:255,G:222,B:173,A:255) </summary>
		public static ColorEngine NavajoWhite { get; } = new ColorEngine(0xffaddeff);

		/// <summary> Color Navy (R:0,G:0,B:128,A:255) </summary>
		public static ColorEngine Navy { get; } = new ColorEngine(0xff800000);

		/// <summary> Color OldLace (R:253,G:245,B:230,A:255) </summary>
		public static ColorEngine OldLace { get; } = new ColorEngine(0xffe6f5fd);

		/// <summary> Color Olive (R:128,G:128,B:0,A:255) </summary>
		public static ColorEngine Olive { get; } = new ColorEngine(0xff008080);

		/// <summary> Color OliveDrab (R:107,G:142,B:35,A:255) </summary>
		public static ColorEngine OliveDrab { get; } = new ColorEngine(0xff238e6b);

		/// <summary> Color Orange (R:255,G:165,B:0,A:255) </summary>
		public static ColorEngine Orange { get; } = new ColorEngine(0xff00a5ff);

		/// <summary> Color OrangeRed (R:255,G:69,B:0,A:255) </summary>
		public static ColorEngine OrangeRed { get; } = new ColorEngine(0xff0045ff);

		/// <summary> Color Orchid (R:218,G:112,B:214,A:255) </summary>
		public static ColorEngine Orchid { get; } = new ColorEngine(0xffd670da);

		/// <summary> Color PaleGoldenrod (R:238,G:232,B:170,A:255) </summary>
		public static ColorEngine PaleGoldenrod { get; } = new ColorEngine(0xffaae8ee);

		/// <summary> Color PaleGreen (R:152,G:251,B:152,A:255) </summary>
		public static ColorEngine PaleGreen { get; } = new ColorEngine(0xff98fb98);

		/// <summary> Color PaleTurquoise (R:175,G:238,B:238,A:255) </summary>
		public static ColorEngine PaleTurquoise { get; } = new ColorEngine(0xffeeeeaf);

		/// <summary> Color PaleVioletRed (R:219,G:112,B:147,A:255) </summary>
		public static ColorEngine PaleVioletRed { get; } = new ColorEngine(0xff9370db);

		/// <summary> Color PapayaWhip (R:255,G:239,B:213,A:255) </summary>
		public static ColorEngine PapayaWhip { get; } = new ColorEngine(0xffd5efff);

		/// <summary> Color PeachPuff (R:255,G:218,B:185,A:255) </summary>
		public static ColorEngine PeachPuff { get; } = new ColorEngine(0xffb9daff);

		/// <summary> Color Peru (R:205,G:133,B:63,A:255) </summary>
		public static ColorEngine Peru { get; } = new ColorEngine(0xff3f85cd);

		/// <summary> Color Pink (R:255,G:192,B:203,A:255) </summary>
		public static ColorEngine Pink { get; } = new ColorEngine(0xffcbc0ff);

		/// <summary> Color Plum (R:221,G:160,B:221,A:255) </summary>
		public static ColorEngine Plum { get; } = new ColorEngine(0xffdda0dd);

		/// <summary> Color PowderBlue (R:176,G:224,B:230,A:255) </summary>
		public static ColorEngine PowderBlue { get; } = new ColorEngine(0xffe6e0b0);

		/// <summary> Color Purple (R:128,G:0,B:128,A:255) </summary>
		public static ColorEngine Purple { get; } = new ColorEngine(0xff800080);

		/// <summary> Color Red (R:255,G:0,B:0,A:255) </summary>
		public static ColorEngine Red { get; } = new ColorEngine(0xff0000ff);

		/// <summary> Color RosyBrown (R:188,G:143,B:143,A:255) </summary>
		public static ColorEngine RosyBrown { get; } = new ColorEngine(0xff8f8fbc);

		/// <summary> Color RoyalBlue (R:65,G:105,B:225,A:255) </summary>
		public static ColorEngine RoyalBlue { get; } = new ColorEngine(0xffe16941);

		/// <summary> Color SaddleBrown (R:139,G:69,B:19,A:255) </summary>
		public static ColorEngine SaddleBrown { get; } = new ColorEngine(0xff13458b);
    	 
		/// <summary> Color Salmon (R:250,G:128,B:114,A:255) </summary>
		public static ColorEngine Salmon { get; } = new ColorEngine(0xff7280fa);
        
		/// <summary> Color SandyBrown (R:244,G:164,B:96,A:255) </summary>
		public static ColorEngine SandyBrown { get; } = new ColorEngine(0xff60a4f4);
        
		/// <summary> Color SeaGreen (R:46,G:139,B:87,A:255) </summary>
		public static ColorEngine SeaGreen { get; } = new ColorEngine(0xff578b2e);
        
		/// <summary> Color SeaShell (R:255,G:245,B:238,A:255) </summary>
		public static ColorEngine SeaShell { get; } = new ColorEngine(0xffeef5ff);
        
		/// <summary> Color Sienna (R:160,G:82,B:45,A:255) </summary>
		public static ColorEngine Sienna { get; } = new ColorEngine(0xff2d52a0);
        
		/// <summary> Color Silver (R:192,G:192,B:192,A:255) </summary>
		public static ColorEngine Silver { get; }  = new ColorEngine(0xffc0c0c0);
        
		/// <summary> Color SkyBlue (R:135,G:206,B:235,A:255) </summary>
		public static ColorEngine SkyBlue { get; } = new ColorEngine(0xffebce87);
       
		/// <summary> Color SlateBlue (R:106,G:90,B:205,A:255) </summary>
		public static ColorEngine SlateBlue { get; } = new ColorEngine(0xffcd5a6a);
      
		/// <summary> Color SlateGray (R:112,G:128,B:144,A:255) </summary>
		public static ColorEngine SlateGray { get; } = new ColorEngine(0xff908070);
      
		/// <summary> Color Snow (R:255,G:250,B:250,A:255) </summary>
		public static ColorEngine Snow { get; } = new ColorEngine(0xfffafaff);
      
		/// <summary> Color SpringGreen (R:0,G:255,B:127,A:255) </summary>
		public static ColorEngine SpringGreen { get; } = new ColorEngine(0xff7fff00);
      
		/// <summary> Color SteelBlue (R:70,G:130,B:180,A:255) </summary>
		public static ColorEngine SteelBlue { get; } = new ColorEngine(0xffb48246);
      
		/// <summary> Color Tan (R:210,G:180,B:140,A:255) </summary>
		public static ColorEngine Tan { get; } = new ColorEngine(0xff8cb4d2);
       
		/// <summary> Color Teal (R:0,G:128,B:128,A:255) </summary>
		public static ColorEngine Teal { get; } = new ColorEngine(0xff808000);
       
		/// <summary> Color Thistle (R:216,G:191,B:216,A:255) </summary>
		public static ColorEngine Thistle { get; } = new ColorEngine(0xffd8bfd8);
       
		/// <summary> Color Tomato (R:255,G:99,B:71,A:255) </summary>
		public static ColorEngine Tomato { get; } = new ColorEngine(0xff4763ff);
        
		/// <summary> Color Turquoise (R:64,G:224,B:208,A:255) </summary>
		public static ColorEngine Turquoise { get; } = new ColorEngine(0xffd0e040);
        
		/// <summary> Color Violet (R:238,G:130,B:238,A:255) </summary>
		public static ColorEngine Violet { get; } = new ColorEngine(0xffee82ee);
        
		/// <summary> Color Wheat (R:245,G:222,B:179,A:255) </summary>
		public static ColorEngine Wheat { get; } = new ColorEngine(0xffb3def5);
	
		/// <summary> Color White (R:255,G:255,B:255,A:255) </summary>
		public static ColorEngine White { get; } = new ColorEngine(uint.MaxValue);
       
		/// <summary> Color WhiteSmoke (R:245,G:245,B:245,A:255) </summary>
		public static ColorEngine WhiteSmoke { get; } = new ColorEngine(0xfff5f5f5);
        
		/// <summary> Color Yellow (R:255,G:255,B:0,A:255) </summary>
		public static ColorEngine Yellow { get; } = new ColorEngine(0xff00ffff);
        
		/// <summary> Color YellowGreen (R:154,G:205,B:50,A:255) </summary>
		public static ColorEngine YellowGreen { get; } = new ColorEngine(0xff32cd9a);
	}
}