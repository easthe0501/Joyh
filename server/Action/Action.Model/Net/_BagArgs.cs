//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: protos/_BagArgs.proto
// Note: requires additional types generated from: protobuf-net.proto
namespace Action.Model
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"LoadBagArgs")]
  public partial class LoadBagArgs : global::ProtoBuf.IExtensible
  {
    public LoadBagArgs() {}
    

    private int _GoodsBagSize = default(int);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"GoodsBagSize", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int GoodsBagSize
    {
      get { return _GoodsBagSize; }
      set { _GoodsBagSize = value; }
    }

    private int _MaterialsBagSize = default(int);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"MaterialsBagSize", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int MaterialsBagSize
    {
      get { return _MaterialsBagSize; }
      set { _MaterialsBagSize = value; }
    }

    private Action.Model.BagsArgs _Bags = null;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"Bags", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public Action.Model.BagsArgs Bags
    {
      get { return _Bags; }
      set { _Bags = value; }
    }

    private int _Money = default(int);
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"Money", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int Money
    {
      get { return _Money; }
      set { _Money = value; }
    }

    private int _Gold = default(int);
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"Gold", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int Gold
    {
      get { return _Gold; }
      set { _Gold = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"BagItemArgs")]
  public partial class BagItemArgs : global::ProtoBuf.IExtensible
  {
    public BagItemArgs() {}
    

    private int _Id = default(int);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"Id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int Id
    {
      get { return _Id; }
      set { _Id = value; }
    }

    private int _SettingId = default(int);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"SettingId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int SettingId
    {
      get { return _SettingId; }
      set { _SettingId = value; }
    }

    private int _SortId = default(int);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"SortId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int SortId
    {
      get { return _SortId; }
      set { _SortId = value; }
    }

    private int _Quantity = default(int);
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"Quantity", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int Quantity
    {
      get { return _Quantity; }
      set { _Quantity = value; }
    }

    private Action.Model.BagType _WhichBag = Action.Model.BagType.GoodBag;
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"WhichBag", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(Action.Model.BagType.GoodBag)]
    public Action.Model.BagType WhichBag
    {
      get { return _WhichBag; }
      set { _WhichBag = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"SellAbandonArgs")]
  public partial class SellAbandonArgs : global::ProtoBuf.IExtensible
  {
    public SellAbandonArgs() {}
    

    private int _Id = default(int);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"Id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int Id
    {
      get { return _Id; }
      set { _Id = value; }
    }

    private Action.Model.BagType _whichBag = Action.Model.BagType.GoodBag;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"whichBag", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(Action.Model.BagType.GoodBag)]
    public Action.Model.BagType whichBag
    {
      get { return _whichBag; }
      set { _whichBag = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"BagsArgs")]
  public partial class BagsArgs : global::ProtoBuf.IExtensible
  {
    public BagsArgs() {}
    
    private readonly global::System.Collections.Generic.List<Action.Model.BagItemArgs> _GoodsBag = new global::System.Collections.Generic.List<Action.Model.BagItemArgs>();
    [global::ProtoBuf.ProtoMember(1, Name=@"GoodsBag", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Action.Model.BagItemArgs> GoodsBag
    {
      get { return _GoodsBag; }
    }
  
    private readonly global::System.Collections.Generic.List<Action.Model.BagItemArgs> _MaterialsBag = new global::System.Collections.Generic.List<Action.Model.BagItemArgs>();
    [global::ProtoBuf.ProtoMember(2, Name=@"MaterialsBag", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Action.Model.BagItemArgs> MaterialsBag
    {
      get { return _MaterialsBag; }
    }
  
    private readonly global::System.Collections.Generic.List<Action.Model.BagItemArgs> _TempBag = new global::System.Collections.Generic.List<Action.Model.BagItemArgs>();
    [global::ProtoBuf.ProtoMember(3, Name=@"TempBag", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Action.Model.BagItemArgs> TempBag
    {
      get { return _TempBag; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ExpandBagArgs")]
  public partial class ExpandBagArgs : global::ProtoBuf.IExtensible
  {
    public ExpandBagArgs() {}
    

    private int _Capacity = default(int);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"Capacity", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int Capacity
    {
      get { return _Capacity; }
      set { _Capacity = value; }
    }

    private Action.Model.BagType _whichBag = Action.Model.BagType.GoodBag;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"whichBag", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(Action.Model.BagType.GoodBag)]
    public Action.Model.BagType whichBag
    {
      get { return _whichBag; }
      set { _whichBag = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"AddItemArgs")]
  public partial class AddItemArgs : global::ProtoBuf.IExtensible
  {
    public AddItemArgs() {}
    

    private Action.Model.BagItemArgs _Item = null;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"Item", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public Action.Model.BagItemArgs Item
    {
      get { return _Item; }
      set { _Item = value; }
    }

    private Action.Model.BagType _WhichBag = Action.Model.BagType.GoodBag;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"WhichBag", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(Action.Model.BagType.GoodBag)]
    public Action.Model.BagType WhichBag
    {
      get { return _WhichBag; }
      set { _WhichBag = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"MoveItemArgs")]
  public partial class MoveItemArgs : global::ProtoBuf.IExtensible
  {
    public MoveItemArgs() {}
    

    private int _Id = default(int);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"Id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int Id
    {
      get { return _Id; }
      set { _Id = value; }
    }

    private Action.Model.BagType _whichBag = Action.Model.BagType.GoodBag;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"whichBag", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(Action.Model.BagType.GoodBag)]
    public Action.Model.BagType whichBag
    {
      get { return _whichBag; }
      set { _whichBag = value; }
    }

    private int _TargetSortId = default(int);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"TargetSortId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int TargetSortId
    {
      get { return _TargetSortId; }
      set { _TargetSortId = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"BagEquipsArgs")]
  public partial class BagEquipsArgs : global::ProtoBuf.IExtensible
  {
    public BagEquipsArgs() {}
    
    private readonly global::System.Collections.Generic.List<Action.Model.BagItemArgs> _Equips = new global::System.Collections.Generic.List<Action.Model.BagItemArgs>();
    [global::ProtoBuf.ProtoMember(1, Name=@"Equips", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Action.Model.BagItemArgs> Equips
    {
      get { return _Equips; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"UseItemsArgs")]
  public partial class UseItemsArgs : global::ProtoBuf.IExtensible
  {
    public UseItemsArgs() {}
    
    private readonly global::System.Collections.Generic.List<int> _ItemIds = new global::System.Collections.Generic.List<int>();
    [global::ProtoBuf.ProtoMember(1, Name=@"ItemIds", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public global::System.Collections.Generic.List<int> ItemIds
    {
      get { return _ItemIds; }
    }
  

    private int _HeroId = default(int);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"HeroId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int HeroId
    {
      get { return _HeroId; }
      set { _HeroId = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"BagItemCollectionArgs")]
  public partial class BagItemCollectionArgs : global::ProtoBuf.IExtensible
  {
    public BagItemCollectionArgs() {}
    
    private readonly global::System.Collections.Generic.List<Action.Model.BagItemArgs> _Items = new global::System.Collections.Generic.List<Action.Model.BagItemArgs>();
    [global::ProtoBuf.ProtoMember(1, Name=@"Items", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Action.Model.BagItemArgs> Items
    {
      get { return _Items; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
    [global::ProtoBuf.ProtoContract(Name=@"BagType")]
    public enum BagType
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"GoodBag", Value=0)]
      GoodBag = 0,
            
      [global::ProtoBuf.ProtoEnum(Name=@"MaterialBag", Value=1)]
      MaterialBag = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"TempBag", Value=2)]
      TempBag = 2,
            
      [global::ProtoBuf.ProtoEnum(Name=@"AllBag", Value=3)]
      AllBag = 3
    }
  
}