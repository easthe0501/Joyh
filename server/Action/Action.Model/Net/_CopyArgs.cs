//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: protos/_CopyArgs.proto
// Note: requires additional types generated from: protobuf-net.proto
// Note: requires additional types generated from: protos/_PrizeArgs.proto
namespace Action.Model
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CopyArgs")]
  public partial class CopyArgs : global::ProtoBuf.IExtensible
  {
    public CopyArgs() {}
    

    private int _CopyId = default(int);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"CopyId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int CopyId
    {
      get { return _CopyId; }
      set { _CopyId = value; }
    }

    private bool _Finished = default(bool);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"Finished", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool Finished
    {
      get { return _Finished; }
      set { _Finished = value; }
    }

    private bool _Locked = default(bool);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"Locked", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool Locked
    {
      get { return _Locked; }
      set { _Locked = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CopyArgsArray")]
  public partial class CopyArgsArray : global::ProtoBuf.IExtensible
  {
    public CopyArgsArray() {}
    
    private readonly global::System.Collections.Generic.List<Action.Model.CopyArgs> _Copies = new global::System.Collections.Generic.List<Action.Model.CopyArgs>();
    [global::ProtoBuf.ProtoMember(1, Name=@"Copies", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Action.Model.CopyArgs> Copies
    {
      get { return _Copies; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"MemberPosCollection")]
  public partial class MemberPosCollection : global::ProtoBuf.IExtensible
  {
    public MemberPosCollection() {}
    
    private readonly global::System.Collections.Generic.List<Action.Model.MemberPosArgs> _Items = new global::System.Collections.Generic.List<Action.Model.MemberPosArgs>();
    [global::ProtoBuf.ProtoMember(1, Name=@"Items", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Action.Model.MemberPosArgs> Items
    {
      get { return _Items; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"MemberPosArgs")]
  public partial class MemberPosArgs : global::ProtoBuf.IExtensible
  {
    public MemberPosArgs() {}
    

    private string _Player = "";
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"Player", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string Player
    {
      get { return _Player; }
      set { _Player = value; }
    }

    private int _Pos = default(int);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"Pos", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int Pos
    {
      get { return _Pos; }
      set { _Pos = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"EnterCopyArgs")]
  public partial class EnterCopyArgs : global::ProtoBuf.IExtensible
  {
    public EnterCopyArgs() {}
    

    private int _CopyId = default(int);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"CopyId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int CopyId
    {
      get { return _CopyId; }
      set { _CopyId = value; }
    }
    private readonly global::System.Collections.Generic.List<Action.Model.MemberPosArgs> _Members = new global::System.Collections.Generic.List<Action.Model.MemberPosArgs>();
    [global::ProtoBuf.ProtoMember(2, Name=@"Members", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Action.Model.MemberPosArgs> Members
    {
      get { return _Members; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CopyGridArgs")]
  public partial class CopyGridArgs : global::ProtoBuf.IExtensible
  {
    public CopyGridArgs() {}
    

    private Action.Model.GridStyle _Style = Action.Model.GridStyle.Empty;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"Style", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(Action.Model.GridStyle.Empty)]
    public Action.Model.GridStyle Style
    {
      get { return _Style; }
      set { _Style = value; }
    }

    private int _Data1 = default(int);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"Data1", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int Data1
    {
      get { return _Data1; }
      set { _Data1 = value; }
    }

    private int _Data2 = default(int);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"Data2", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int Data2
    {
      get { return _Data2; }
      set { _Data2 = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CopyGridArgsArray")]
  public partial class CopyGridArgsArray : global::ProtoBuf.IExtensible
  {
    public CopyGridArgsArray() {}
    

    private int _CopyId = default(int);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"CopyId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int CopyId
    {
      get { return _CopyId; }
      set { _CopyId = value; }
    }
    private readonly global::System.Collections.Generic.List<Action.Model.CopyGridArgs> _Grids = new global::System.Collections.Generic.List<Action.Model.CopyGridArgs>();
    [global::ProtoBuf.ProtoMember(2, Name=@"Grids", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Action.Model.CopyGridArgs> Grids
    {
      get { return _Grids; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CopyAction")]
  public partial class CopyAction : global::ProtoBuf.IExtensible
  {
    public CopyAction() {}
    

    private string _Player = "";
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"Player", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string Player
    {
      get { return _Player; }
      set { _Player = value; }
    }

    private int _Pos = default(int);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"Pos", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int Pos
    {
      get { return _Pos; }
      set { _Pos = value; }
    }

    private Action.Model.CopyGridArgs _Grid = null;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"Grid", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public Action.Model.CopyGridArgs Grid
    {
      get { return _Grid; }
      set { _Grid = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CopyProxyArgs")]
  public partial class CopyProxyArgs : global::ProtoBuf.IExtensible
  {
    public CopyProxyArgs() {}
    

    private int _CopyId = default(int);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"CopyId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int CopyId
    {
      get { return _CopyId; }
      set { _CopyId = value; }
    }

    private Action.Model.PrizeTip _Prize = null;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"Prize", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public Action.Model.PrizeTip Prize
    {
      get { return _Prize; }
      set { _Prize = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CardArgs")]
  public partial class CardArgs : global::ProtoBuf.IExtensible
  {
    public CardArgs() {}
    

    private int _Quality = default(int);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"Quality", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int Quality
    {
      get { return _Quality; }
      set { _Quality = value; }
    }

    private Action.Model.CardType _Type = Action.Model.CardType.G;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"Type", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(Action.Model.CardType.G)]
    public Action.Model.CardType Type
    {
      get { return _Type; }
      set { _Type = value; }
    }

    private string _Data = "";
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"Data", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string Data
    {
      get { return _Data; }
      set { _Data = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CardArrayArgs")]
  public partial class CardArrayArgs : global::ProtoBuf.IExtensible
  {
    public CardArrayArgs() {}
    
    private readonly global::System.Collections.Generic.List<Action.Model.CardArgs> _Cards = new global::System.Collections.Generic.List<Action.Model.CardArgs>();
    [global::ProtoBuf.ProtoMember(1, Name=@"Cards", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Action.Model.CardArgs> Cards
    {
      get { return _Cards; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
    [global::ProtoBuf.ProtoContract(Name=@"GridStyle")]
    public enum GridStyle
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"Empty", Value=0)]
      Empty = 0,
            
      [global::ProtoBuf.ProtoEnum(Name=@"Money", Value=1)]
      Money = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"Material", Value=2)]
      Material = 2,
            
      [global::ProtoBuf.ProtoEnum(Name=@"Box", Value=8)]
      Box = 8,
            
      [global::ProtoBuf.ProtoEnum(Name=@"Monster", Value=4)]
      Monster = 4,
            
      [global::ProtoBuf.ProtoEnum(Name=@"Random", Value=5)]
      Random = 5,
            
      [global::ProtoBuf.ProtoEnum(Name=@"Boss", Value=6)]
      Boss = 6,
            
      [global::ProtoBuf.ProtoEnum(Name=@"Meeting", Value=7)]
      Meeting = 7,
            
      [global::ProtoBuf.ProtoEnum(Name=@"Card", Value=3)]
      Card = 3
    }
  
    [global::ProtoBuf.ProtoContract(Name=@"CardType")]
    public enum CardType
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"G", Value=0)]
      G = 0,
            
      [global::ProtoBuf.ProtoEnum(Name=@"M", Value=1)]
      M = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"P", Value=2)]
      P = 2,
            
      [global::ProtoBuf.ProtoEnum(Name=@"R", Value=3)]
      R = 3,
            
      [global::ProtoBuf.ProtoEnum(Name=@"E", Value=4)]
      E = 4,
            
      [global::ProtoBuf.ProtoEnum(Name=@"I", Value=5)]
      I = 5
    }
  
}