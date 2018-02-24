using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using Engine.Data.Json;

using UnityEngine;

public static class ObjectExtensions {
    
    public static string ToJson(this object inst) {
        if (inst == null) {
            return null;
        }

        try {
            return JsonMapper.ToJson(inst).FilterJson();
            //return JsonUtility.ToJson(inst).FilterJson();
        }
        catch (Exception e) {
            LogUtil.LogError("ToJson:FAILED:" + e);
            return null;
        }
    }

    public static string ToJson<T>(this T inst) {
        if (inst == null) {
            return null;
        }
        
        try {
            return JsonMapper.ToJson(inst).FilterJson();
            //return JsonUtility.ToJson(inst).FilterJson();
        }
        catch (Exception e) {
            LogUtil.LogError("ToJson:FAILED:" + e);
            return null;
        }
    }

    public static Dictionary<string,object> FromJsonToDict(this string inst) {
        return JsonMapper.ToObject<Dictionary<string, object>>(inst.FilterJson());
        //return JsonUtility.FromJson<Dictionary<string,object>>(inst.FilterJson());
    }

    public static List<Dictionary<string, object>> FromJsonToDictList(this string inst) {
        return JsonMapper.ToObject<List<Dictionary<string, object>>>(inst.FilterJson());
        //return JsonUtility.FromJson<List<Dictionary<string, object>>>(inst.FilterJson());
    }

    public static T FromJson<T>(this string inst) {
        return JsonMapper.ToObject<T>(inst.FilterJson());
        //return JsonUtility.FromJson<T>(inst.FilterJson());
    }
    
    public static object FromJson(this string inst) {
        return JsonMapper.ToObject<object>(inst.FilterJson());
        //return JsonUtility.FromJson<object>(inst.FilterJson());
    }

    //

    public static T ToDataObject<T>(this object val) {
        if (val == null) {
            return default(T);
        }
        return val.ToJson().FromJson<T>();
    }

    public static string FilterJson(this string val) {
        if (string.IsNullOrEmpty(val))
            return val;
        return val
                .Replace("\\\"", "\"")
                .TrimStart('"').TrimEnd('"');
    }
    
    public static object ConvertJson(this string val) {
        if (val.StartsWith("{") || val.StartsWith("[")) {
            try {
                
                if (val.TrimStart().StartsWith("{")) {
                    return val.FilterJson().FromJson<Dictionary<string, object>>();
                }
                else if (val.TrimStart().StartsWith("[")) {
                    return val.FilterJson().FromJson<List<object>>();
                }
                
            }
            catch (Exception e) {
                UnityEngine.Debug.Log("ERROR parsing attribute:" + e);
            }
        }
        
        return val;
    }

    /*
    public static int GetObjectSize(this object val) {
        if (val == null) {
            return 0;
        }

        BinaryFormatter bf = new BinaryFormatter();
        MemoryStream ms = new MemoryStream();
        byte[] Array;
        bf.Serialize(ms, val.GetType());
        Array = ms.ToArray();
        return Array.Length;
    }
    */

    public static long GetObjectSize(this object o) {

        long size = 0;
        using (Stream s = new MemoryStream()) {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(s, o);
            size = s.Length;
        }

        return size;
    }
}