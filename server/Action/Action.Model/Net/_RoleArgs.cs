//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: protos/_RoleArgs.proto
// Note: requires additional types generated from: protobuf-net.proto
namespace Action.Model
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ViewRolePanelArgs")]
  public partial class ViewRolePanelArgs : global::ProtoBuf.IExtensible
  {
    public ViewRolePanelArgs() {}
    

    private string _Name = "";
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"Name", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string Name
    {
      get { return _Name; }
      set { _Name = value; }
    }

    private int _Sex = default(int);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"Sex", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int Sex
    {
      get { return _Sex; }
      set { _Sex = value; }
    }

    private int _Face = default(int);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"Face", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int Face
    {
      get { return _Face; }
      set { _Face = value; }
    }

    private int _Level = default(int);
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"Level", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int Level
    {
      get { return _Level; }
      set { _Level = value; }
    }

    private int _Money = default(int);
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"Money", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int Money
    {
      get { return _Money; }
      set { _Money = value; }
    }

    private int _Gold = default(int);
    [global::ProtoBuf.ProtoMember(6, IsRequired = false, Name=@"Gold", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int Gold
    {
      get { return _Gold; }
      set { _Gold = value; }
    }

    private int _Energy = default(int);
    [global::ProtoBuf.ProtoMember(7, IsRequired = false, Name=@"Energy", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int Energy
    {
      get { return _Energy; }
      set { _Energy = value; }
    }

    private int _Repute = default(int);
    [global::ProtoBuf.ProtoMember(8, IsRequired = false, Name=@"Repute", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int Repute
    {
      get { return _Repute; }
      set { _Repute = value; }
    }

    private int _Title = default(int);
    [global::ProtoBuf.ProtoMember(9, IsRequired = false, Name=@"Title", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int Title
    {
      get { return _Title; }
      set { _Title = value; }
    }

    private int _Exp = default(int);
    [global::ProtoBuf.ProtoMember(10, IsRequired = false, Name=@"Exp", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int Exp
    {
      get { return _Exp; }
      set { _Exp = value; }
    }

    private string _GuildName = "";
    [global::ProtoBuf.ProtoMember(11, IsRequired = false, Name=@"GuildName", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string GuildName
    {
      get { return _GuildName; }
      set { _GuildName = value; }
    }

    private int _Vip = default(int);
    [global::ProtoBuf.ProtoMember(12, IsRequired = false, Name=@"Vip", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int Vip
    {
      get { return _Vip; }
      set { _Vip = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GoldMoneyArgs")]
  public partial class GoldMoneyArgs : global::ProtoBuf.IExtensible
  {
    public GoldMoneyArgs() {}
    

    private int _Gold = default(int);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"Gold", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int Gold
    {
      get { return _Gold; }
      set { _Gold = value; }
    }

    private int _Money = default(int);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"Money", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int Money
    {
      get { return _Money; }
      set { _Money = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GoldEnergyArgs")]
  public partial class GoldEnergyArgs : global::ProtoBuf.IExtensible
  {
    public GoldEnergyArgs() {}
    

    private int _Gold = default(int);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"Gold", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int Gold
    {
      get { return _Gold; }
      set { _Gold = value; }
    }

    private int _Energy = default(int);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"Energy", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int Energy
    {
      get { return _Energy; }
      set { _Energy = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"DailyCountArgs")]
  public partial class DailyCountArgs : global::ProtoBuf.IExtensible
  {
    public DailyCountArgs() {}
    

    private int _BuyMoney = default(int);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"BuyMoney", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int BuyMoney
    {
      get { return _BuyMoney; }
      set { _BuyMoney = value; }
    }

    private int _BuyEnergy = default(int);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"BuyEnergy", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int BuyEnergy
    {
      get { return _BuyEnergy; }
      set { _BuyEnergy = value; }
    }

    private int _ContributeForGuild = default(int);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"ContributeForGuild", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int ContributeForGuild
    {
      get { return _ContributeForGuild; }
      set { _ContributeForGuild = value; }
    }

    private int _Pvp = default(int);
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"Pvp", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int Pvp
    {
      get { return _Pvp; }
      set { _Pvp = value; }
    }

    private int _PvpCount = default(int);
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"PvpCount", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
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
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"VipArgs")]
  public partial class VipArgs : global::ProtoBuf.IExtensible
  {
    public VipArgs() {}
    

    private int _VipLevel = default(int);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"VipLevel", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int VipLevel
    {
      get { return _VipLevel; }
      set { _VipLevel = value; }
    }

    private int _RechargeGold = default(int);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"RechargeGold", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int RechargeGold
    {
      get { return _RechargeGold; }
      set { _RechargeGold = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"LoadSignInArgs")]
  public partial class LoadSignInArgs : global::ProtoBuf.IExtensible
  {
    public LoadSignInArgs() {}
    
    private readonly global::System.Collections.Generic.List<int> _Days = new global::System.Collections.Generic.List<int>();
    [global::ProtoBuf.ProtoMember(1, Name=@"Days", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public global::System.Collections.Generic.List<int> Days
    {
      get { return _Days; }
    }
  

    private int _SumDays = default(int);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"SumDays", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int SumDays
    {
      get { return _SumDays; }
      set { _SumDays = value; }
    }

    private int _ContinueDays = default(int);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"ContinueDays", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int ContinueDays
    {
      get { return _ContinueDays; }
      set { _ContinueDays = value; }
    }
    private readonly global::System.Collections.Generic.List<Action.Model.SignInPrize> _ConPrizes = new global::System.Collections.Generic.List<Action.Model.SignInPrize>();
    [global::ProtoBuf.ProtoMember(4, Name=@"ConPrizes", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Action.Model.SignInPrize> ConPrizes
    {
      get { return _ConPrizes; }
    }
  
    private readonly global::System.Collections.Generic.List<Action.Model.SignInPrize> _SumPrizes = new global::System.Collections.Generic.List<Action.Model.SignInPrize>();
    [global::ProtoBuf.ProtoMember(5, Name=@"SumPrizes", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Action.Model.SignInPrize> SumPrizes
    {
      get { return _SumPrizes; }
    }
  

    private bool _IfSignToday = default(bool);
    [global::ProtoBuf.ProtoMember(6, IsRequired = false, Name=@"IfSignToday", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool IfSignToday
    {
      get { return _IfSignToday; }
      set { _IfSignToday = value; }
    }

    private int _MissTimes = default(int);
    [global::ProtoBuf.ProtoMember(7, IsRequired = false, Name=@"MissTimes", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int MissTimes
    {
      get { return _MissTimes; }
      set { _MissTimes = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"SignInPrize")]
  public partial class SignInPrize : global::ProtoBuf.IExtensible
  {
    public SignInPrize() {}
    

    private int _Index = default(int);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"Index", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int Index
    {
      get { return _Index; }
      set { _Index = value; }
    }

    private bool _IfGet = default(bool);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"IfGet", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool IfGet
    {
      get { return _IfGet; }
      set { _IfGet = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GetSignPrizeArgs")]
  public partial class GetSignPrizeArgs : global::ProtoBuf.IExtensible
  {
    public GetSignPrizeArgs() {}
    

    private int _ConOrSum = default(int);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"ConOrSum", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int ConOrSum
    {
      get { return _ConOrSum; }
      set { _ConOrSum = value; }
    }

    private int _Index = default(int);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"Index", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int Index
    {
      get { return _Index; }
      set { _Index = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"LoadBuyMoneyArgs")]
  public partial class LoadBuyMoneyArgs : global::ProtoBuf.IExtensible
  {
    public LoadBuyMoneyArgs() {}
    

    private int _BuyMoneyCount = default(int);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"BuyMoneyCount", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int BuyMoneyCount
    {
      get { return _BuyMoneyCount; }
      set { _BuyMoneyCount = value; }
    }

    private int _Gold = default(int);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"Gold", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int Gold
    {
      get { return _Gold; }
      set { _Gold = value; }
    }

    private int _Money = default(int);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"Money", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int Money
    {
      get { return _Money; }
      set { _Money = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"LoadBuyEnergyArgs")]
  public partial class LoadBuyEnergyArgs : global::ProtoBuf.IExtensible
  {
    public LoadBuyEnergyArgs() {}
    

    private int _BuyEnergyCount = default(int);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"BuyEnergyCount", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int BuyEnergyCount
    {
      get { return _BuyEnergyCount; }
      set { _BuyEnergyCount = value; }
    }

    private int _Gold = default(int);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"Gold", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int Gold
    {
      get { return _Gold; }
      set { _Gold = value; }
    }

    private int _Energy = default(int);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"Energy", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int Energy
    {
      get { return _Energy; }
      set { _Energy = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}