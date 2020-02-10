﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ES3Types;

namespace ES3Internal
{
	public static class ES3TypeMgr
	{
		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		public static Dictionary<Type, ES3Type> types = null;

		public static ES3Type GetOrCreateES3Type(Type type, bool throwException = true)
		{
			if(types == null)
				Init();

			ES3Type es3Type;
			// If type doesn't exist, create one.
			if(types.TryGetValue(type, out es3Type))
				return es3Type;
			return CreateES3Type(type, throwException);
		}

		public static ES3Type GetES3Type(Type type)
		{
			if(types == null)
				Init();

			ES3Type es3Type;
			if(types.TryGetValue(type, out es3Type))
				return es3Type;
			return null;
		}

		internal static ES3Type CreateES3Type(Type type, bool throwException = true)
		{
			ES3Type es3Type;

			if(ES3Reflection.IsEnum(type))
				return new ES3Type_enum(type);
			else if(ES3Reflection.TypeIsArray(type))
			{
				int rank = ES3Reflection.GetArrayRank(type);

				if(rank == 1)
					es3Type = new ES3ArrayType(type);
				else if(rank == 2)
					es3Type = new ES32DArrayType(type);
				else if(rank == 3)
					es3Type = new ES33DArrayType(type);
				else if(throwException)
					throw new NotSupportedException("Only arrays with up to three dimensions are supported by Easy Save.");
				else
					return null;
			}
			else if(ES3Reflection.IsGenericType(type))
			{
				Type genericType = ES3Reflection.GetGenericTypeDefinition(type);
				if(genericType == typeof(List<>))
					es3Type = new ES3ListType(type);
				else if(genericType == typeof(Dictionary<,>))
					es3Type = new ES3DictionaryType(type);
				else if(genericType == typeof(Queue<>))
					es3Type = new ES3QueueType(type);
				else if(genericType == typeof(Stack<>))
					es3Type = new ES3StackType(type);
				else if(throwException)
					throw new NotSupportedException("Generic type \""+type.ToString()+"\" is not supported by Easy Save.");
				else
					return null;
			}
			else if(ES3Reflection.IsPrimitive(type)) // ERROR: We should not have to create an ES3Type for a primitive.
			{
				if(types == null || types.Count == 0)	// If the type list is not initialised, it is most likely an initialisation error.
					throw new TypeLoadException("ES3Type for primitive could not be found, and the type list is empty. Please contact Easy Save developers at http://www.moodkie.com/contact");
				else // Else it's a different error, possibly an error in the specific ES3Type for that type.
					throw new TypeLoadException("ES3Type for primitive could not be found, but the type list has been initialised and is not empty. Please contact Easy Save developers on mail@moodkie.com");
			}
			else
			{
				if(ES3Reflection.IsAssignableFrom(typeof(Component), type))
					es3Type = new ES3ReflectedComponentType(type);
				else if(ES3Reflection.IsValueType(type))
					es3Type = new ES3ReflectedValueType(type);
				else if(ES3Reflection.IsAssignableFrom(typeof(ScriptableObject), type))
					es3Type = new ES3ReflectedScriptableObjectType(type);
				else if(ES3Reflection.HasParameterlessConstructor(type))
					es3Type = new ES3ReflectedObjectType(type);
				else if(throwException)
					throw new NotSupportedException("Type of "+type+" is not supported as it does not have a parameterless constructor. Only value types, Components or ScriptableObjects are supportable without a parameterless constructor. However, you may be able to create an ES3Type script to add support for it.");
				else
					return null;
			}

			if(throwException && es3Type.type == null)
			{
				Debug.Log(string.Format("ES3Type.type is null when trying to create an ES3Type for {0}.", type));
				return null;
			}

			types.Add(type, es3Type);
			return es3Type;
		}

		internal static void Init()
		{
			var typeList = ES3Reflection.GetInstances<ES3Type>();
			types = new Dictionary<Type, ES3Type>(typeList.Count);

			for(int i=0; i<typeList.Count; i++)
				types[typeList[i].type] = typeList[i];

			// Check that the type list was initialised correctly.
			if(types == null || types.Count == 0)
				throw new TypeLoadException("Type list could not be initialised. Please contact Easy Save developers on mail@moodkie.com.");
		}
	}
}
