//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: protos/_ArenaArgs.proto
// Note: requires additional types generated from: protobuf-net.proto
// Note: requires additional types generated from: protos/BattleReport.proto
namespace Action.Model
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"LoadArenaArgs")]
  public partial class LoadArenaArgs : global::ProtoBuf.IExtensible
  {
    public LoadArenaArgs() {}
    

    private int _ArenaScore = default(int);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"ArenaScore", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int ArenaScore
    {
      get { return _ArenaScore; }
      set { _ArenaScore = value; }
    }

    private int _ArenaRank = default(int);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"ArenaRank", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int ArenaRank
    {
      get { return _ArenaRank; }
      set { _ArenaRank = value; }
    }

    private int _ArenaBestRank = default(int);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"ArenaBestRank", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int ArenaBestRank
    {
      get { return _ArenaBestRank; }
      set { _ArenaBestRank = value; }
    }

    private int _PvpCount = default(int);
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"PvpCount", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int PvpCount
    {
      get { return _PvpCount; }
      set { _PvpCount = value; }
    }

    private int _BuyPvpCount = default(int);
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"BuyPvpCount", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int BuyPvpCount
    {
      get { return _BuyPvpCount; }
      set { _BuyPvpCount = value; }
    }
    private readonly global::System.Collections.Generic.List<Action.Model.ArenaLogArgs> _ArenaLog = new global::System.Collections.Generic.List<Action.Model.ArenaLogArgs>();
    [global::ProtoBuf.ProtoMember(6, Name=@"ArenaLog", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Action.Model.ArenaLogArgs> ArenaLog
    {
      get { return _ArenaLog; }
    }
  
    private readonly global::System.Collections.Generic.List<Action.Model.ArenaTargetArgs> _ArenaTargets = new global::System.Collections.Generic.List<Action.Model.ArenaTargetArgs>();
    [global::ProtoBuf.ProtoMember(7, Name=@"ArenaTargets", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Action.Model.ArenaTargetArgs> ArenaTargets
    {
      get { return _ArenaTargets; }
    }
  

    private Action.Model.ArenaPrize _Prize = null;
    [global::ProtoBuf.ProtoMember(8, IsRequired = false, Name=@"Prize", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public Action.Model.ArenaPrize Prize
    {
      get { return _Prize; }
      set { _Prize = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"LoadTargetsArgs")]
  public partial class LoadTargetsArgs : global::ProtoBuf.IExtensible
  {
    public LoadTargetsArgs() {}
    
    private readonly global::System.Collections.Generic.List<Action.Model.ArenaTargetArgs> _ArenaTargets = new global::System.Collections.Generic.List<Action.Model.ArenaTargetArgs>();
    [global::ProtoBuf.ProtoMember(1, Name=@"ArenaTargets", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Action.Model.ArenaTargetArgs> ArenaTargets
    {
      get { return _ArenaTargets; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ArenaLogArgs")]
  public partial class ArenaLogArgs : global::ProtoBuf.IExtensible
  {
    public ArenaLogArgs() {}
    

    private int _CreateTime = default(int);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"CreateTime", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int CreateTime
    {
      get { return _CreateTime; }
      set { _CreateTime = value; }
    }

    private string _ArenaPlayer = "";
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"ArenaPlayer", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string ArenaPlayer
    {
      get { return _ArenaPlayer; }
      set { _ArenaPlayer = value; }
    }

    private string _TargetPlayer = "";
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"TargetPlayer", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string TargetPlayer
    {
      get { return _TargetPlayer; }
      set { _TargetPlayer = value; }
    }

    private bool _WinOrLose = default(bool);
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"WinOrLose", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool WinOrLose
    {
      get { return _WinOrLose; }
      set { _WinOrLose = value; }
    }

    private string _ReportId = "";
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"ReportId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string ReportId
    {
      get { return _ReportId; }
      set { _ReportId = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ArenaTargetArgs")]
  public partial class ArenaTargetArgs : global::ProtoBuf.IExtensible
  {
    public ArenaTargetArgs() {}
    

    private string _TargetName = "";
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"TargetName", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string TargetName
    {
      get { return _TargetName; }
      set { _TargetName = value; }
    }

    private int _TargetLevel = default(int);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"TargetLevel", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int TargetLevel
    {
      get { return _TargetLevel; }
      set { _TargetLevel = value; }
    }

    private int _ArenaRank = default(int);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"ArenaRank", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int ArenaRank
    {
      get { return _ArenaRank; }
      set { _ArenaRank = value; }
    }

    private int _Sex = default(int);
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"Sex", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int Sex
    {
      get { return _Sex; }
      set { _Sex = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ArenaPrize")]
  public partial class ArenaPrize : global::ProtoBuf.IExtensible
  {
    public ArenaPrize() {}
    

    private int _Money = default(int);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"Money", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int Money
    {
      get { return _Money; }
      set { _Money = value; }
    }

    private int _Repute = default(int);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"Repute", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int Repute
    {
      get { return _Repute; }
      set { _Repute = value; }
    }

    private int _Gold = default(int);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"Gold", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
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
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"LoadTop10")]
  public partial class LoadTop10 : global::ProtoBuf.IExtensible
  {
    public LoadTop10() {}
    
    private readonly global::System.Collections.Generic.List<Action.Model.Top10Args> _Top10 = new global::System.Collections.Generic.List<Action.Model.Top10Args>();
    [global::ProtoBuf.ProtoMember(1, Name=@"Top10", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Action.Model.Top10Args> Top10
    {
      get { return _Top10; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"Top10Args")]
  public partial class Top10Args : global::ProtoBuf.IExtensible
  {
    public Top10Args() {}
    

    private int _Rank = default(int);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"Rank", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int Rank
    {
      get { return _Rank; }
      set { _Rank = value; }
    }

    private string _Name = "";
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"Name", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string Name
    {
      get { return _Name; }
      set { _Name = value; }
    }

    private int _Level = default(int);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"Level", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int Level
    {
      get { return _Level; }
      set { _Level = value; }
    }

    private int _Sex = default(int);
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"Sex", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int Sex
    {
      get { return _Sex; }
      set { _Sex = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RefreshArenaArgs")]
  public partial class RefreshArenaArgs : global::ProtoBuf.IExtensible
  {
    public RefreshArenaArgs() {}
    

    private int _ArenaRank = default(int);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"ArenaRank", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int ArenaRank
    {
      get { return _ArenaRank; }
      set { _ArenaRank = value; }
    }

    private int _ArenaBestRank = default(int);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"ArenaBestRank", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int ArenaBestRank
    {
      get { return _ArenaBestRank; }
      set { _ArenaBestRank = value; }
    }

    private int _PvpCount = default(int);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"PvpCount", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int PvpCount
    {
      get { return _PvpCount; }
      set { _PvpCount = value; }
    }
    private readonly global::System.Collections.Generic.List<Action.Model.ArenaLogArgs> _ArenaLog = new global::System.Collections.Generic.List<Action.Model.ArenaLogArgs>();
    [global::ProtoBuf.ProtoMember(4, Name=@"ArenaLog", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Action.Model.ArenaLogArgs> ArenaLog
    {
      get { return _ArenaLog; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"BuyPvpCountArgs")]
  public partial class BuyPvpCountArgs : global::ProtoBuf.IExtensible
  {
    public BuyPvpCountArgs() {}
    

    private int _BuyPvpCount = default(int);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"BuyPvpCount", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int BuyPvpCount
    {
      get { return _BuyPvpCount; }
      set { _BuyPvpCount = value; }
    }

    private int _PvpCount = default(int);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"PvpCount", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int PvpCount
    {
      get { return _PvpCount; }
      set { _PvpCount = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}