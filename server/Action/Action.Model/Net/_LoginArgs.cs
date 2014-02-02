//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: protos/_LoginArgs.proto
// Note: requires additional types generated from: protobuf-net.proto
namespace Action.Model
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"BackdoorLoginArgs")]
  public partial class BackdoorLoginArgs : global::ProtoBuf.IExtensible
  {
    public BackdoorLoginArgs() {}
    

    private string _Account = "";
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"Account", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string Account
    {
      get { return _Account; }
      set { _Account = value; }
    }

    private string _Password = "";
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"Password", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string Password
    {
      get { return _Password; }
      set { _Password = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"TencentApiArgs")]
  public partial class TencentApiArgs : global::ProtoBuf.IExtensible
  {
    public TencentApiArgs() {}
    

    private string _OpenId = "";
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"OpenId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string OpenId
    {
      get { return _OpenId; }
      set { _OpenId = value; }
    }

    private string _OpenKey = "";
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"OpenKey", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string OpenKey
    {
      get { return _OpenKey; }
      set { _OpenKey = value; }
    }

    private string _Pf = "";
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"Pf", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string Pf
    {
      get { return _Pf; }
      set { _Pf = value; }
    }

    private string _PfKey = "";
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"PfKey", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string PfKey
    {
      get { return _PfKey; }
      set { _PfKey = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CreateRoleArgs")]
  public partial class CreateRoleArgs : global::ProtoBuf.IExtensible
  {
    public CreateRoleArgs() {}
    

    private string _Name = "";
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"Name", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string Name
    {
      get { return _Name; }
      set { _Name = value; }
    }

    private int _Job = default(int);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"Job", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int Job
    {
      get { return _Job; }
      set { _Job = value; }
    }

    private int _Sex = default(int);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"Sex", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int Sex
    {
      get { return _Sex; }
      set { _Sex = value; }
    }

    private int _Face = default(int);
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"Face", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int Face
    {
      get { return _Face; }
      set { _Face = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"EnterGameArgs")]
  public partial class EnterGameArgs : global::ProtoBuf.IExtensible
  {
    public EnterGameArgs() {}
    

    private string _Key = "";
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"Key", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string Key
    {
      get { return _Key; }
      set { _Key = value; }
    }

    private string _Account = "";
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"Account", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string Account
    {
      get { return _Account; }
      set { _Account = value; }
    }

    private string _Player = "";
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"Player", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string Player
    {
      get { return _Player; }
      set { _Player = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}