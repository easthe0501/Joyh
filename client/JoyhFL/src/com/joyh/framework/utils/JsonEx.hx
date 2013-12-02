package com.joyh.framework.utils;

/**
 * ...
 * @author hd
 */
class JsonEx
{
	public static function encode(o : Dynamic) {
		// to solve some of the issues above you should iterate on all the fields,
		// check for a non-compatible Json type and build a structure like the
		// following before serializing
		return haxe.Json.stringify({
		  type : Type.getClassName(Type.getClass(o)),
		  data : o
		});
	  }

	  public static function decode<T>(s : String) : T {
		var o = haxe.Json.parse(s),
			inst = Type.createEmptyInstance(Type.resolveClass(o.type));
		populate(inst, o.data);
		return inst;
	  }

	  static function populate(inst, data) {
		for(field in Reflect.fields(data)) {
		  Reflect.setField(inst, field, Reflect.field(data, field));
		}
	  }
	
}