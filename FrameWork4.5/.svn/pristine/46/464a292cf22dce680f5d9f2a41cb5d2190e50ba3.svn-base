using System;
using System.Data;
using System.Text;
using System.IO;

using Sigbit.Common;
using Sigbit.Data;

namespace Sigbit.Framework
{
    /// <summary>
    /// 用户信息表(sbt_user)的表操作用户类
    /// </summary>
    public class TbUser : TbUserBase
    {
        #region 用户可编辑区域

        /// <summary>
        /// 通过用户名取出记录
        /// </summary>
        /// <param name="sUserName">用户名</param>
        public void FetchByUserName(string sUserName)
        {
            FetchByUserName(sUserName, false);
        }

        /// <summary>
        /// 通过用户名取出记录
        /// </summary>
        /// <param name="sUserName">用户名</param>
        /// <param name="bAllowNoData">是否允许没有记录</param>
        /// <returns>是否取到记录</returns>
        public bool FetchByUserName(string sUserName, bool bAllowNoData)
        {
            string sSQL = "select * from sbt_user where user_name = " + Quote(sUserName);
            DataSet ds = DataHelper.Instance.ExecuteDataSet(sSQL);
            int nRecordCount = ds.Tables[0].Rows.Count;
            if (nRecordCount <= 0)
            {
                if (bAllowNoData)
                    return false;
                else
                    throw new Exception("TbUser.Fetch() Error : cannot locate record via UserName" 
                            + " - " + sUserName);
            }
            if (nRecordCount > 1)
                throw new Exception("TbUser.FetchByUserName error: 查到了两条同用户名的数据 - "
                        + sUserName);
            AssignByDataRow(ds, 0);
            return true;
        }

        /// <summary>
        /// 删除
        /// </summary>
        public override void Delete()
        {
            base.Delete();
            TbRoleUser.DeleteAllRolesOfUser(this.UserUid);
        }

        /// <summary>
        /// 得到一个部门的用户数
        /// </summary>
        /// <param name="sDeptId">部门标识</param>
        /// <returns>用户数</returns>
        public static int GetUserCountOfDept(string sDeptId)
        {
            string sSQL = "select count(*) from sbt_user "
                    + " where dept_id = " + StringUtil.QuotedToDBStr(sDeptId);
            int nRet = ConvertUtil.ToInt(DataHelper.Instance.ExecuteScalar(sSQL));
            return nRet;
        }
        /// ... Add your code here ... ///
        /// void FetchByXXX(...)
        /// static DataSet GetDataSetByXXX(...)
        /// static void DeleteByXXX(...)

        #endregion
    }

    /// <summary>
    /// 用户信息表(sbt_user)的字段名类
    /// </summary>
    public class TbUserF
    {
        public const string TableName = "sbt_user";
        public const string UserUid = "user_uid";
        public const string UserName = "user_name";
        public const string Password = "password";
        public const string RealName = "real_name";
        public const string StaffCode = "staff_code";
        public const string ThirdPartyCode = "third_party_code";
        public const string Sex = "sex";
        public const string Birthday = "birthday";
        public const string EduLevelCode = "edu_level_code";
        public const string DeptId = "dept_id";
        public const string OrgName = "org_name";
        public const string JobTitleCode = "job_title_code";
        public const string WorkTypeCode = "work_type_code";
        public const string PositionCode = "position_code";
        public const string IdCardTypeCode = "id_card_type_code";
        public const string IdCard = "id_card";
        public const string Address = "address";
        public const string ZipCode = "zip_code";
        public const string Telephone = "telephone";
        public const string Mobilephone = "mobilephone";
        public const string Email = "email";
        public const string LoginCount = "login_count";
        public const string LastLoginTime = "last_login_time";
        public const string CreateTime = "create_time";
        public const string Creator = "creator";
        public const string IsActive = "is_active";
        public const string IsAdmin = "is_admin";
        public const string Remarks = "remarks";
        public const string ExInfo01 = "ex_info_01";
        public const string ExInfo02 = "ex_info_02";
        public const string ExInfo03 = "ex_info_03";
        public const string ExInfo04 = "ex_info_04";
        public const string ExInfo05 = "ex_info_05";
        public const string ExInfo06 = "ex_info_06";
        public const string ExInfo07 = "ex_info_07";
        public const string ExInfo08 = "ex_info_08";
        public const string ExInfo09 = "ex_info_09";
        public const string ExInfo10 = "ex_info_10";
    }


    /// <summary>
    /// 用户信息表(sbt_user)的表操作基类
    /// </summary>
    public class TbUserBase : Sigbit.Data.TableBase
    {
        #region 私有变量定义
        protected string _userUid = "";
        protected string _userName = "";
        protected string _password = "";
        protected string _realName = "";
        protected string _staffCode = "";
        protected string _thirdPartyCode = "";
        protected string _sex = "";
        protected string _birthday = "";
        protected string _eduLevelCode = "";
        protected string _deptId = "";
        protected string _orgName = "";
        protected string _jobTitleCode = "";
        protected string _workTypeCode = "";
        protected string _positionCode = "";
        protected string _idCardTypeCode = "";
        protected string _idCard = "";
        protected string _address = "";
        protected string _zipCode = "";
        protected string _telephone = "";
        protected string _mobilephone = "";
        protected string _email = "";
        protected int _loginCount;
        protected string _lastLoginTime = "";
        protected string _createTime = "";
        protected string _creator = "";
        protected string _isActive = "";
        protected string _isAdmin = "";
        protected string _remarks = "";
        protected string _exInfo01 = "";
        protected string _exInfo02 = "";
        protected string _exInfo03 = "";
        protected string _exInfo04 = "";
        protected string _exInfo05 = "";
        protected string _exInfo06 = "";
        protected string _exInfo07 = "";
        protected string _exInfo08 = "";
        protected string _exInfo09 = "";
        protected string _exInfo10 = "";
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public TbUserBase()
        {
            ResetData();
        }

        #region 属性定义
        /// <summary>
        /// 用户ID，主键
        /// </summary>
        public string UserUid
        {
            get
            {
                return _userUid;
            }
            set
            {
                _userUid = value;
            }
        }

        /// <summary>
        /// 帐户名(uidx)
        /// </summary>
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
            }
        }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }

        /// <summary>
        /// 实名
        /// </summary>
        public string RealName
        {
            get
            {
                return _realName;
            }
            set
            {
                _realName = value;
            }
        }

        /// <summary>
        /// 员工号
        /// </summary>
        public string StaffCode
        {
            get
            {
                return _staffCode;
            }
            set
            {
                _staffCode = value;
            }
        }

        /// <summary>
        /// 第三方员工标识
        /// </summary>
        public string ThirdPartyCode
        {
            get
            {
                return _thirdPartyCode;
            }
            set
            {
                _thirdPartyCode = value;
            }
        }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex
        {
            get
            {
                return _sex;
            }
            set
            {
                _sex = value;
            }
        }

        /// <summary>
        /// 用户生日
        /// </summary>
        public string Birthday
        {
            get
            {
                return _birthday;
            }
            set
            {
                _birthday = value;
            }
        }

        /// <summary>
        /// 学历代码
        /// </summary>
        public string EduLevelCode
        {
            get
            {
                return _eduLevelCode;
            }
            set
            {
                _eduLevelCode = value;
            }
        }

        /// <summary>
        /// 部门ID
        /// </summary>
        public string DeptId
        {
            get
            {
                return _deptId;
            }
            set
            {
                _deptId = value;
            }
        }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string OrgName
        {
            get
            {
                return _orgName;
            }
            set
            {
                _orgName = value;
            }
        }

        /// <summary>
        /// 职务代码
        /// </summary>
        public string JobTitleCode
        {
            get
            {
                return _jobTitleCode;
            }
            set
            {
                _jobTitleCode = value;
            }
        }

        /// <summary>
        /// 工作性质，行业
        /// </summary>
        public string WorkTypeCode
        {
            get
            {
                return _workTypeCode;
            }
            set
            {
                _workTypeCode = value;
            }
        }

        /// <summary>
        /// 岗位代码
        /// </summary>
        public string PositionCode
        {
            get
            {
                return _positionCode;
            }
            set
            {
                _positionCode = value;
            }
        }

        /// <summary>
        /// 证件类型
        /// </summary>
        public string IdCardTypeCode
        {
            get
            {
                return _idCardTypeCode;
            }
            set
            {
                _idCardTypeCode = value;
            }
        }

        /// <summary>
        /// 证件号
        /// </summary>
        public string IdCard
        {
            get
            {
                return _idCard;
            }
            set
            {
                _idCard = value;
            }
        }

        /// <summary>
        /// 住址
        /// </summary>
        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
            }
        }

        /// <summary>
        /// 邮编
        /// </summary>
        public string ZipCode
        {
            get
            {
                return _zipCode;
            }
            set
            {
                _zipCode = value;
            }
        }

        /// <summary>
        /// 电话
        /// </summary>
        public string Telephone
        {
            get
            {
                return _telephone;
            }
            set
            {
                _telephone = value;
            }
        }

        /// <summary>
        /// 移动电话
        /// </summary>
        public string Mobilephone
        {
            get
            {
                return _mobilephone;
            }
            set
            {
                _mobilephone = value;
            }
        }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
            }
        }

        /// <summary>
        /// 登录次数
        /// </summary>
        public int LoginCount
        {
            get
            {
                return _loginCount;
            }
            set
            {
                _loginCount = value;
            }
        }

        /// <summary>
        /// 最后一次登录时间
        /// </summary>
        public string LastLoginTime
        {
            get
            {
                return _lastLoginTime;
            }
            set
            {
                _lastLoginTime = value;
            }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime
        {
            get
            {
                return _createTime;
            }
            set
            {
                _createTime = value;
            }
        }

        /// <summary>
        /// 创建者
        /// </summary>
        public string Creator
        {
            get
            {
                return _creator;
            }
            set
            {
                _creator = value;
            }
        }

        /// <summary>
        /// 是否激活
        /// </summary>
        public string IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                _isActive = value;
            }
        }

        /// <summary>
        /// 是否是超级管理员
        /// </summary>
        public string IsAdmin
        {
            get
            {
                return _isAdmin;
            }
            set
            {
                _isAdmin = value;
            }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks
        {
            get
            {
                return _remarks;
            }
            set
            {
                _remarks = value;
            }
        }

        /// <summary>
        /// 扩展信息一
        /// </summary>
        public string ExInfo01
        {
            get
            {
                return _exInfo01;
            }
            set
            {
                _exInfo01 = value;
            }
        }

        /// <summary>
        /// 扩展信息二
        /// </summary>
        public string ExInfo02
        {
            get
            {
                return _exInfo02;
            }
            set
            {
                _exInfo02 = value;
            }
        }

        /// <summary>
        /// 扩展信息三
        /// </summary>
        public string ExInfo03
        {
            get
            {
                return _exInfo03;
            }
            set
            {
                _exInfo03 = value;
            }
        }

        /// <summary>
        /// 扩展信息四
        /// </summary>
        public string ExInfo04
        {
            get
            {
                return _exInfo04;
            }
            set
            {
                _exInfo04 = value;
            }
        }

        /// <summary>
        /// 扩展信息五
        /// </summary>
        public string ExInfo05
        {
            get
            {
                return _exInfo05;
            }
            set
            {
                _exInfo05 = value;
            }
        }

        /// <summary>
        /// 扩展信息六
        /// </summary>
        public string ExInfo06
        {
            get
            {
                return _exInfo06;
            }
            set
            {
                _exInfo06 = value;
            }
        }

        /// <summary>
        /// 扩展信息七
        /// </summary>
        public string ExInfo07
        {
            get
            {
                return _exInfo07;
            }
            set
            {
                _exInfo07 = value;
            }
        }

        /// <summary>
        /// 扩展信息八
        /// </summary>
        public string ExInfo08
        {
            get
            {
                return _exInfo08;
            }
            set
            {
                _exInfo08 = value;
            }
        }

        /// <summary>
        /// 扩展信息九
        /// </summary>
        public string ExInfo09
        {
            get
            {
                return _exInfo09;
            }
            set
            {
                _exInfo09 = value;
            }
        }

        /// <summary>
        /// 扩展信息十
        /// </summary>
        public string ExInfo10
        {
            get
            {
                return _exInfo10;
            }
            set
            {
                _exInfo10 = value;
            }
        }
        #endregion

        #region 变量的清零及输出
        /// <summary>
        /// 得到数据的HTML显示文本
        /// </summary>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("UserUid: " + this._userUid + "<br>");
            sb.Append("UserName: " + this._userName + "<br>");
            sb.Append("Password: " + this._password + "<br>");
            sb.Append("RealName: " + this._realName + "<br>");
            sb.Append("StaffCode: " + this._staffCode + "<br>");
            sb.Append("ThirdPartyCode: " + this._thirdPartyCode + "<br>");
            sb.Append("Sex: " + this._sex + "<br>");
            sb.Append("Birthday: " + this._birthday + "<br>");
            sb.Append("EduLevelCode: " + this._eduLevelCode + "<br>");
            sb.Append("DeptId: " + this._deptId + "<br>");
            sb.Append("OrgName: " + this._orgName + "<br>");
            sb.Append("JobTitleCode: " + this._jobTitleCode + "<br>");
            sb.Append("WorkTypeCode: " + this._workTypeCode + "<br>");
            sb.Append("PositionCode: " + this._positionCode + "<br>");
            sb.Append("IdCardTypeCode: " + this._idCardTypeCode + "<br>");
            sb.Append("IdCard: " + this._idCard + "<br>");
            sb.Append("Address: " + this._address + "<br>");
            sb.Append("ZipCode: " + this._zipCode + "<br>");
            sb.Append("Telephone: " + this._telephone + "<br>");
            sb.Append("Mobilephone: " + this._mobilephone + "<br>");
            sb.Append("Email: " + this._email + "<br>");
            sb.Append("LoginCount: " + this._loginCount + "<br>");
            sb.Append("LastLoginTime: " + this._lastLoginTime + "<br>");
            sb.Append("CreateTime: " + this._createTime + "<br>");
            sb.Append("Creator: " + this._creator + "<br>");
            sb.Append("IsActive: " + this._isActive + "<br>");
            sb.Append("IsAdmin: " + this._isAdmin + "<br>");
            sb.Append("Remarks: " + this._remarks + "<br>");
            sb.Append("ExInfo01: " + this._exInfo01 + "<br>");
            sb.Append("ExInfo02: " + this._exInfo02 + "<br>");
            sb.Append("ExInfo03: " + this._exInfo03 + "<br>");
            sb.Append("ExInfo04: " + this._exInfo04 + "<br>");
            sb.Append("ExInfo05: " + this._exInfo05 + "<br>");
            sb.Append("ExInfo06: " + this._exInfo06 + "<br>");
            sb.Append("ExInfo07: " + this._exInfo07 + "<br>");
            sb.Append("ExInfo08: " + this._exInfo08 + "<br>");
            sb.Append("ExInfo09: " + this._exInfo09 + "<br>");
            sb.Append("ExInfo10: " + this._exInfo10 + "<br>");
            return sb.ToString();
        }

        /// <summary>
        /// 清零本记录的数据
        /// </summary>
        public override void ResetData()
        {
            _userUid = "";
            _userName = "";
            _password = "";
            _realName = "";
            _staffCode = "";
            _thirdPartyCode = "";
            _sex = "";
            _birthday = "";
            _eduLevelCode = "";
            _deptId = "";
            _orgName = "";
            _jobTitleCode = "";
            _workTypeCode = "";
            _positionCode = "";
            _idCardTypeCode = "";
            _idCard = "";
            _address = "";
            _zipCode = "";
            _telephone = "";
            _mobilephone = "";
            _email = "";
            _loginCount = 0;
            _lastLoginTime = "";
            _createTime = "";
            _creator = "";
            _isActive = "";
            _isAdmin = "";
            _remarks = "";
            _exInfo01 = "";
            _exInfo02 = "";
            _exInfo03 = "";
            _exInfo04 = "";
            _exInfo05 = "";
            _exInfo06 = "";
            _exInfo07 = "";
            _exInfo08 = "";
            _exInfo09 = "";
            _exInfo10 = "";
        }

        #endregion

        #region 基本的增删改查操作
        /// <summary>
        /// 按主键获取一条数据（如无数据抛例外）
        /// </summary>
        public override void Fetch()
        {
            Fetch(false);
        }

        /// <summary>
        /// 按主键获取一条数据
        /// </summary>
        /// <returns>是否访问到数据</returns>
        public override bool Fetch(bool allowNoData)
        {
            string sSQL;
            int nRecordCount;
            DataSet dataSet;
            DataRow row;

            //======= 1. 得到SQL语句 ==============
            sSQL = "select user_name,           password,            real_name,            \n";
            sSQL += "       staff_code,          third_party_code,    sex,                  \n";
            sSQL += "       birthday,            edu_level_code,      dept_id,              \n";
            sSQL += "       org_name,            job_title_code,      work_type_code,       \n";
            sSQL += "       position_code,       id_card_type_code,   id_card,              \n";
            sSQL += "       address,             zip_code,            telephone,            \n";
            sSQL += "       mobilephone,         email,               login_count,          \n";
            sSQL += "       last_login_time,     create_time,         creator,              \n";
            sSQL += "       is_active,           is_admin,            remarks,              \n";
            sSQL += "       ex_info_01,          ex_info_02,          ex_info_03,           \n";
            sSQL += "       ex_info_04,          ex_info_05,          ex_info_06,           \n";
            sSQL += "       ex_info_07,          ex_info_08,          ex_info_09,           \n";
            sSQL += "       ex_info_10           \n";
            sSQL += "  from sbt_user    \n";
            sSQL += "  where user_uid = " + Quote(_userUid) + "\n";

            //======= 2. 运行SQL语句 ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;
            if (nRecordCount != 1)
            {
                if (!allowNoData)
                    throw new Exception("TbUser.Fetch() Error - cannot locate record via PrimaryKey.");
                else
                    return false;
            }

            //======= 3. 读取记录 ========
            row = dataSet.Tables[0].Rows[0];
            _userName = DbToString(row["user_name"]);
            _password = DbToString(row["password"]);
            _realName = DbToString(row["real_name"]);
            _staffCode = DbToString(row["staff_code"]);
            _thirdPartyCode = DbToString(row["third_party_code"]);
            _sex = DbToString(row["sex"]);
            _birthday = DbToString(row["birthday"]);
            _eduLevelCode = DbToString(row["edu_level_code"]);
            _deptId = DbToString(row["dept_id"]);
            _orgName = DbToString(row["org_name"]);
            _jobTitleCode = DbToString(row["job_title_code"]);
            _workTypeCode = DbToString(row["work_type_code"]);
            _positionCode = DbToString(row["position_code"]);
            _idCardTypeCode = DbToString(row["id_card_type_code"]);
            _idCard = DbToString(row["id_card"]);
            _address = DbToString(row["address"]);
            _zipCode = DbToString(row["zip_code"]);
            _telephone = DbToString(row["telephone"]);
            _mobilephone = DbToString(row["mobilephone"]);
            _email = DbToString(row["email"]);
            _loginCount = DbToInt(row["login_count"]);
            _lastLoginTime = DbToString(row["last_login_time"]);
            _createTime = DbToString(row["create_time"]);
            _creator = DbToString(row["creator"]);
            _isActive = DbToString(row["is_active"]);
            _isAdmin = DbToString(row["is_admin"]);
            _remarks = DbToString(row["remarks"]);
            _exInfo01 = DbToString(row["ex_info_01"]);
            _exInfo02 = DbToString(row["ex_info_02"]);
            _exInfo03 = DbToString(row["ex_info_03"]);
            _exInfo04 = DbToString(row["ex_info_04"]);
            _exInfo05 = DbToString(row["ex_info_05"]);
            _exInfo06 = DbToString(row["ex_info_06"]);
            _exInfo07 = DbToString(row["ex_info_07"]);
            _exInfo08 = DbToString(row["ex_info_08"]);
            _exInfo09 = DbToString(row["ex_info_09"]);
            _exInfo10 = DbToString(row["ex_info_10"]);
            return true;
        }

        /// <summary>
        /// 插入一条数据
        /// </summary>
        public override void Insert()
        {
            string sSQL;

            sSQL = "insert into sbt_user \n";
            sSQL += "( user_uid,         user_name,        \n";
            sSQL += "  password,         real_name,        \n";
            sSQL += "  staff_code,       third_party_code, \n";
            sSQL += "  sex,              birthday,         \n";
            sSQL += "  edu_level_code,   dept_id,          \n";
            sSQL += "  org_name,         job_title_code,   \n";
            sSQL += "  work_type_code,   position_code,    \n";
            sSQL += "  id_card_type_code, id_card,          \n";
            sSQL += "  address,          zip_code,         \n";
            sSQL += "  telephone,        mobilephone,      \n";
            sSQL += "  email,            login_count,      \n";
            sSQL += "  last_login_time,  create_time,      \n";
            sSQL += "  creator,          is_active,        \n";
            sSQL += "  is_admin,         remarks,          \n";
            sSQL += "  ex_info_01,       ex_info_02,       \n";
            sSQL += "  ex_info_03,       ex_info_04,       \n";
            sSQL += "  ex_info_05,       ex_info_06,       \n";
            sSQL += "  ex_info_07,       ex_info_08,       \n";
            sSQL += "  ex_info_09,       ex_info_10        \n";
            sSQL += ") values (          \n";
            sSQL += Quote(_userUid) + "," + Quote(_userName) + ",\n";
            sSQL += Quote(_password) + "," + Quote(_realName) + ",\n";
            sSQL += Quote(_staffCode) + "," + Quote(_thirdPartyCode) + ",\n";
            sSQL += Quote(_sex) + "," + Quote(_birthday) + ",\n";
            sSQL += Quote(_eduLevelCode) + "," + Quote(_deptId) + ",\n";
            sSQL += Quote(_orgName) + "," + Quote(_jobTitleCode) + ",\n";
            sSQL += Quote(_workTypeCode) + "," + Quote(_positionCode) + ",\n";
            sSQL += Quote(_idCardTypeCode) + "," + Quote(_idCard) + ",\n";
            sSQL += Quote(_address) + "," + Quote(_zipCode) + ",\n";
            sSQL += Quote(_telephone) + "," + Quote(_mobilephone) + ",\n";
            sSQL += Quote(_email) + "," + _loginCount.ToString() + ",\n";
            sSQL += Quote(_lastLoginTime) + "," + Quote(_createTime) + ",\n";
            sSQL += Quote(_creator) + "," + Quote(_isActive) + ",\n";
            sSQL += Quote(_isAdmin) + "," + Quote(_remarks) + ",\n";
            sSQL += Quote(_exInfo01) + "," + Quote(_exInfo02) + ",\n";
            sSQL += Quote(_exInfo03) + "," + Quote(_exInfo04) + ",\n";
            sSQL += Quote(_exInfo05) + "," + Quote(_exInfo06) + ",\n";
            sSQL += Quote(_exInfo07) + "," + Quote(_exInfo08) + ",\n";
            sSQL += Quote(_exInfo09) + "," + Quote(_exInfo10) + ")\n";

            DataHelper.Instance.ExecuteNonQuery(sSQL);
        }

        /// <summary>
        /// 按主键删除一条数据
        /// </summary>
        public override void Delete()
        {
            string sSQL;

            sSQL = "delete from sbt_user \n";
            sSQL += "  where user_uid = " + Quote(_userUid) + "\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbUser.Delete() Error - cannot update data via PrimaryKey.");
        }

        /// <summary>
        /// 按主键更新一条数据
        /// </summary>
        public override void Update()
        {
            string sSQL;

            sSQL = "update sbt_user set \n";
            sSQL += " user_uid = " + Quote(_userUid) + ",\n";
            sSQL += " user_name = " + Quote(_userName) + ",\n";
            sSQL += " password = " + Quote(_password) + ",\n";
            sSQL += " real_name = " + Quote(_realName) + ",\n";
            sSQL += " staff_code = " + Quote(_staffCode) + ",\n";
            sSQL += " third_party_code = " + Quote(_thirdPartyCode) + ",\n";
            sSQL += " sex = " + Quote(_sex) + ",\n";
            sSQL += " birthday = " + Quote(_birthday) + ",\n";
            sSQL += " edu_level_code = " + Quote(_eduLevelCode) + ",\n";
            sSQL += " dept_id = " + Quote(_deptId) + ",\n";
            sSQL += " org_name = " + Quote(_orgName) + ",\n";
            sSQL += " job_title_code = " + Quote(_jobTitleCode) + ",\n";
            sSQL += " work_type_code = " + Quote(_workTypeCode) + ",\n";
            sSQL += " position_code = " + Quote(_positionCode) + ",\n";
            sSQL += " id_card_type_code = " + Quote(_idCardTypeCode) + ",\n";
            sSQL += " id_card = " + Quote(_idCard) + ",\n";
            sSQL += " address = " + Quote(_address) + ",\n";
            sSQL += " zip_code = " + Quote(_zipCode) + ",\n";
            sSQL += " telephone = " + Quote(_telephone) + ",\n";
            sSQL += " mobilephone = " + Quote(_mobilephone) + ",\n";
            sSQL += " email = " + Quote(_email) + ",\n";
            sSQL += " login_count = " + _loginCount.ToString() + ",\n";
            sSQL += " last_login_time = " + Quote(_lastLoginTime) + ",\n";
            sSQL += " create_time = " + Quote(_createTime) + ",\n";
            sSQL += " creator = " + Quote(_creator) + ",\n";
            sSQL += " is_active = " + Quote(_isActive) + ",\n";
            sSQL += " is_admin = " + Quote(_isAdmin) + ",\n";
            sSQL += " remarks = " + Quote(_remarks) + ",\n";
            sSQL += " ex_info_01 = " + Quote(_exInfo01) + ",\n";
            sSQL += " ex_info_02 = " + Quote(_exInfo02) + ",\n";
            sSQL += " ex_info_03 = " + Quote(_exInfo03) + ",\n";
            sSQL += " ex_info_04 = " + Quote(_exInfo04) + ",\n";
            sSQL += " ex_info_05 = " + Quote(_exInfo05) + ",\n";
            sSQL += " ex_info_06 = " + Quote(_exInfo06) + ",\n";
            sSQL += " ex_info_07 = " + Quote(_exInfo07) + ",\n";
            sSQL += " ex_info_08 = " + Quote(_exInfo08) + ",\n";
            sSQL += " ex_info_09 = " + Quote(_exInfo09) + ",\n";
            sSQL += " ex_info_10 = " + Quote(_exInfo10) + "\n";
            sSQL += "  where user_uid = " + Quote(_userUid) + "\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            //if (nRowsAffected != 1)
            //    throw new Exception("TbUser.Update() Error - cannot update data via PrimaryKey.");
        }

        /// <summary>
        /// 按主键判断记录是否存在
        /// </summary>
        /// <returns>记录是否存在</returns>
        public override bool RecordExists()
        {
            string sSQL;
            int nRecordCount;

            //======= 1. 得到SQL语句 ==============
            sSQL = "select count(*) from sbt_user \n";
            sSQL += "  where user_uid = " + Quote(_userUid) + "\n";

            //======= 2. 运行SQL语句 ========
            nRecordCount = (int)DataHelper.Instance.ExecuteScalar(sSQL);
            if (nRecordCount == 1)
                return true;
            else
                return false;
        }

        #endregion

        #region 以主键为参数的操作
        /// <summary>
        /// 以主键为参数获取一条数据（如无数据抛例外）
        /// </summary>
        public void FetchByE(string userUid)
        {
            bool hasData;
            hasData = FetchBy(userUid);
            if (!hasData)
                throw new Exception("TbUser.FetchBy(...) Error - cannot locate record via PrimaryKey.");
        }

        /// <summary>
        /// 以主键为参数获取一条数据
        /// </summary>
        /// <returns>是否访问到数据</returns>
        public bool FetchBy(string userUid)
        {
            _userUid = userUid;

            return Fetch(true);
        }

        /// <summary>
        /// 以主键为参数获取数据，并创建类的实例
        /// </summary>
        /// <returns>类的实例</returns>
        static public TbUser CreateBy(string userUid)
        {
            TbUser tbl;
            bool hasData;

            tbl = new TbUser();
            hasData = tbl.FetchBy(userUid);

            if (!hasData)
                return null;
            else
                return tbl;
        }

        /// <summary>
        /// 以主键为参数删除一条数据
        /// </summary>
        static public void DeleteBy(string userUid)
        {
            TbUser tbl;
            tbl = new TbUser();

            tbl.UserUid = userUid;

            tbl.Delete();
        }

        #endregion

        #region 文件和访问组件相关的支持函数
        /// <summary>
        /// 通过DataRow进行赋值
        /// </summary>
        public override void AssignByDataRow(DataRow row)
        {
            _userUid = DbToString(row["user_uid"]);
            _userName = DbToString(row["user_name"]);
            _password = DbToString(row["password"]);
            _realName = DbToString(row["real_name"]);
            _staffCode = DbToString(row["staff_code"]);
            _thirdPartyCode = DbToString(row["third_party_code"]);
            _sex = DbToString(row["sex"]);
            _birthday = DbToString(row["birthday"]);
            _eduLevelCode = DbToString(row["edu_level_code"]);
            _deptId = DbToString(row["dept_id"]);
            _orgName = DbToString(row["org_name"]);
            _jobTitleCode = DbToString(row["job_title_code"]);
            _workTypeCode = DbToString(row["work_type_code"]);
            _positionCode = DbToString(row["position_code"]);
            _idCardTypeCode = DbToString(row["id_card_type_code"]);
            _idCard = DbToString(row["id_card"]);
            _address = DbToString(row["address"]);
            _zipCode = DbToString(row["zip_code"]);
            _telephone = DbToString(row["telephone"]);
            _mobilephone = DbToString(row["mobilephone"]);
            _email = DbToString(row["email"]);
            _loginCount = DbToInt(row["login_count"]);
            _lastLoginTime = DbToString(row["last_login_time"]);
            _createTime = DbToString(row["create_time"]);
            _creator = DbToString(row["creator"]);
            _isActive = DbToString(row["is_active"]);
            _isAdmin = DbToString(row["is_admin"]);
            _remarks = DbToString(row["remarks"]);
            _exInfo01 = DbToString(row["ex_info_01"]);
            _exInfo02 = DbToString(row["ex_info_02"]);
            _exInfo03 = DbToString(row["ex_info_03"]);
            _exInfo04 = DbToString(row["ex_info_04"]);
            _exInfo05 = DbToString(row["ex_info_05"]);
            _exInfo06 = DbToString(row["ex_info_06"]);
            _exInfo07 = DbToString(row["ex_info_07"]);
            _exInfo08 = DbToString(row["ex_info_08"]);
            _exInfo09 = DbToString(row["ex_info_09"]);
            _exInfo10 = DbToString(row["ex_info_10"]);
        }

        /// <summary>
        /// 通过DataSet进行赋值
        /// </summary>
        /// <param name="dataSet">数据集</param>
        /// <param name="rowNum">行号</param>
        public override void AssignByDataRow(DataSet dataSet, int rowNum)
        {
            DataRow row;
            row = dataSet.Tables[0].Rows[rowNum];

            AssignByDataRow(row);
        }

        /// <summary>
        /// 将当前记录的信息保存到文件
        /// </summary>
        /// </param name="fileName">保存到的文件名</param>
        public override void DumpToFile(string fileName)
        {
            //========= 1. 打开文件 ============
            StreamWriter writer;
            string sLine;
            writer = File.CreateText(fileName);

            //========= 2. 写入文件 ============
            sLine = "user_uid\x9" + _userUid;
            writer.WriteLine(sLine);

            sLine = "user_name\x9" + _userName;
            writer.WriteLine(sLine);

            sLine = "password\x9" + _password;
            writer.WriteLine(sLine);

            sLine = "real_name\x9" + _realName;
            writer.WriteLine(sLine);

            sLine = "staff_code\x9" + _staffCode;
            writer.WriteLine(sLine);

            sLine = "third_party_code\x9" + _thirdPartyCode;
            writer.WriteLine(sLine);

            sLine = "sex\x9" + _sex;
            writer.WriteLine(sLine);

            sLine = "birthday\x9" + _birthday;
            writer.WriteLine(sLine);

            sLine = "edu_level_code\x9" + _eduLevelCode;
            writer.WriteLine(sLine);

            sLine = "dept_id\x9" + _deptId;
            writer.WriteLine(sLine);

            sLine = "org_name\x9" + _orgName;
            writer.WriteLine(sLine);

            sLine = "job_title_code\x9" + _jobTitleCode;
            writer.WriteLine(sLine);

            sLine = "work_type_code\x9" + _workTypeCode;
            writer.WriteLine(sLine);

            sLine = "position_code\x9" + _positionCode;
            writer.WriteLine(sLine);

            sLine = "id_card_type_code\x9" + _idCardTypeCode;
            writer.WriteLine(sLine);

            sLine = "id_card\x9" + _idCard;
            writer.WriteLine(sLine);

            sLine = "address\x9" + _address;
            writer.WriteLine(sLine);

            sLine = "zip_code\x9" + _zipCode;
            writer.WriteLine(sLine);

            sLine = "telephone\x9" + _telephone;
            writer.WriteLine(sLine);

            sLine = "mobilephone\x9" + _mobilephone;
            writer.WriteLine(sLine);

            sLine = "email\x9" + _email;
            writer.WriteLine(sLine);

            sLine = "login_count\x9" + _loginCount.ToString();
            writer.WriteLine(sLine);

            sLine = "last_login_time\x9" + _lastLoginTime;
            writer.WriteLine(sLine);

            sLine = "create_time\x9" + _createTime;
            writer.WriteLine(sLine);

            sLine = "creator\x9" + _creator;
            writer.WriteLine(sLine);

            sLine = "is_active\x9" + _isActive;
            writer.WriteLine(sLine);

            sLine = "is_admin\x9" + _isAdmin;
            writer.WriteLine(sLine);

            sLine = "remarks\x9" + _remarks;
            writer.WriteLine(sLine);

            sLine = "ex_info_01\x9" + _exInfo01;
            writer.WriteLine(sLine);

            sLine = "ex_info_02\x9" + _exInfo02;
            writer.WriteLine(sLine);

            sLine = "ex_info_03\x9" + _exInfo03;
            writer.WriteLine(sLine);

            sLine = "ex_info_04\x9" + _exInfo04;
            writer.WriteLine(sLine);

            sLine = "ex_info_05\x9" + _exInfo05;
            writer.WriteLine(sLine);

            sLine = "ex_info_06\x9" + _exInfo06;
            writer.WriteLine(sLine);

            sLine = "ex_info_07\x9" + _exInfo07;
            writer.WriteLine(sLine);

            sLine = "ex_info_08\x9" + _exInfo08;
            writer.WriteLine(sLine);

            sLine = "ex_info_09\x9" + _exInfo09;
            writer.WriteLine(sLine);

            sLine = "ex_info_10\x9" + _exInfo10;
            writer.WriteLine(sLine);

            //========= 3. 关闭文件 ============
            writer.Flush();
            writer.Close();
        }

        /// <summary>
        /// 将表中的所有记录保存到文件
        /// </summary>
        /// </param name="fileName">保存到的文件名</param>
        public override void DumpAllRecordsToFile(string fileName)
        {
            string sSQL;
            int i, nCol, nRecordCount;
            DataSet dataSet;
            DataRow row;
            string sFieldValue, sLine;
            StreamWriter writer;

            //======== 1. 打开文件 ========
            writer = File.CreateText(fileName);

            //======== 2. 写入第一行（标题行） ========
            sLine = "user_uid\tuser_name\tpassword\t";
            sLine += "real_name\tstaff_code\tthird_party_code\t";
            sLine += "sex\tbirthday\tedu_level_code\t";
            sLine += "dept_id\torg_name\tjob_title_code\t";
            sLine += "work_type_code\tposition_code\tid_card_type_code\t";
            sLine += "id_card\taddress\tzip_code\t";
            sLine += "telephone\tmobilephone\temail\t";
            sLine += "login_count\tlast_login_time\tcreate_time\t";
            sLine += "creator\tis_active\tis_admin\t";
            sLine += "remarks\tex_info_01\tex_info_02\t";
            sLine += "ex_info_03\tex_info_04\tex_info_05\t";
            sLine += "ex_info_06\tex_info_07\tex_info_08\t";
            sLine += "ex_info_09\tex_info_10";
            writer.WriteLine(sLine);

            //======== 3. 得到SQL语句 ========
            sSQL = "select user_uid,        user_name,       password,        \n";
            sSQL += "       real_name,       staff_code,      third_party_code,\n";
            sSQL += "       sex,             birthday,        edu_level_code,  \n";
            sSQL += "       dept_id,         org_name,        job_title_code,  \n";
            sSQL += "       work_type_code,  position_code,   id_card_type_code,\n";
            sSQL += "       id_card,         address,         zip_code,        \n";
            sSQL += "       telephone,       mobilephone,     email,           \n";
            sSQL += "       login_count,     last_login_time, create_time,     \n";
            sSQL += "       creator,         is_active,       is_admin,        \n";
            sSQL += "       remarks,         ex_info_01,      ex_info_02,      \n";
            sSQL += "       ex_info_03,      ex_info_04,      ex_info_05,      \n";
            sSQL += "       ex_info_06,      ex_info_07,      ex_info_08,      \n";
            sSQL += "       ex_info_09,      ex_info_10      \n";
            sSQL += "  from sbt_user";

            //======== 4. 运行SQL语句 ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;

            //======== 5. 处理每一行 ========
            for (i = 0; i < nRecordCount; i++)
            {
                row = dataSet.Tables[0].Rows[i];
                sLine = "";

                //======== 5.1 处理每一列 ========
                for (nCol = 0; nCol < 38; nCol++)
                {
                    if (row[nCol] is DBNull)
                        sFieldValue = "";
                    else
                        sFieldValue = row[nCol].ToString();

                    if (nCol == 0)
                        sLine += sFieldValue;
                    else
                        sLine += "\t" + sFieldValue;
                }

                //======== 5.2 将一行的值写入文件 ========
                writer.WriteLine(sLine);
            }

            //======== 6. 关闭文件 ========
            writer.Flush();
            writer.Close();
        }

        #endregion

        #region 辅助支持函数
        /// <summary>
        /// 获取一个字段的数据库类型
        /// </summary>
        static public string DBTypeOfFieldName(string fieldName)
        {
            switch (fieldName)
            {
                case "user_uid":
                    return "varchar";
                case "user_name":
                    return "varchar";
                case "password":
                    return "varchar";
                case "real_name":
                    return "varchar";
                case "staff_code":
                    return "varchar";
                case "third_party_code":
                    return "varchar";
                case "sex":
                    return "varchar";
                case "birthday":
                    return "varchar";
                case "edu_level_code":
                    return "varchar";
                case "dept_id":
                    return "varchar";
                case "org_name":
                    return "varchar";
                case "job_title_code":
                    return "varchar";
                case "work_type_code":
                    return "varchar";
                case "position_code":
                    return "varchar";
                case "id_card_type_code":
                    return "varchar";
                case "id_card":
                    return "varchar";
                case "address":
                    return "varchar";
                case "zip_code":
                    return "varchar";
                case "telephone":
                    return "varchar";
                case "mobilephone":
                    return "varchar";
                case "email":
                    return "varchar";
                case "login_count":
                    return "int";
                case "last_login_time":
                    return "varchar";
                case "create_time":
                    return "varchar";
                case "creator":
                    return "varchar";
                case "is_active":
                    return "varchar";
                case "is_admin":
                    return "varchar";
                case "remarks":
                    return "varchar";
                case "ex_info_01":
                    return "text";
                case "ex_info_02":
                    return "text";
                case "ex_info_03":
                    return "varchar";
                case "ex_info_04":
                    return "varchar";
                case "ex_info_05":
                    return "varchar";
                case "ex_info_06":
                    return "varchar";
                case "ex_info_07":
                    return "varchar";
                case "ex_info_08":
                    return "varchar";
                case "ex_info_09":
                    return "varchar";
                case "ex_info_10":
                    return "varchar";
                default:
                    throw new Exception("TbUserBase.DBTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
            }
        }

        /// <summary>
        /// 获取一个字段的CSharp类型
        /// </summary>
        static public string CSharpTypeOfFieldName(string fieldName)
        {
            switch (fieldName)
            {
                case "user_uid":
                    return "string";
                case "user_name":
                    return "string";
                case "password":
                    return "string";
                case "real_name":
                    return "string";
                case "staff_code":
                    return "string";
                case "third_party_code":
                    return "string";
                case "sex":
                    return "string";
                case "birthday":
                    return "string";
                case "edu_level_code":
                    return "string";
                case "dept_id":
                    return "string";
                case "org_name":
                    return "string";
                case "job_title_code":
                    return "string";
                case "work_type_code":
                    return "string";
                case "position_code":
                    return "string";
                case "id_card_type_code":
                    return "string";
                case "id_card":
                    return "string";
                case "address":
                    return "string";
                case "zip_code":
                    return "string";
                case "telephone":
                    return "string";
                case "mobilephone":
                    return "string";
                case "email":
                    return "string";
                case "login_count":
                    return "int";
                case "last_login_time":
                    return "string";
                case "create_time":
                    return "string";
                case "creator":
                    return "string";
                case "is_active":
                    return "string";
                case "is_admin":
                    return "string";
                case "remarks":
                    return "string";
                case "ex_info_01":
                    return "string";
                case "ex_info_02":
                    return "string";
                case "ex_info_03":
                    return "string";
                case "ex_info_04":
                    return "string";
                case "ex_info_05":
                    return "string";
                case "ex_info_06":
                    return "string";
                case "ex_info_07":
                    return "string";
                case "ex_info_08":
                    return "string";
                case "ex_info_09":
                    return "string";
                case "ex_info_10":
                    return "string";
                default:
                    throw new Exception("TbUserBase.CSharpTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
            }
        }

        /// <summary>
        /// 通过一个字段的值访问得到另一个字段的值
        /// </summary>
        static public string GetFieldValueBy(string fromFieldName, string fromFieldValue, string toFieldName)
        {
            string sSQL;
            int nRecordCount;
            DataSet dataSet;
            DataRow row;

            //======= 1. 得到SQL语句 ==============
            sSQL = "select " + toFieldName + "from sbt_user \n";
            sSQL += "where " + fromFieldName + " = ";
            if (CSharpTypeOfFieldName(fromFieldName) == "string")
                sSQL += "'" + fromFieldValue + "'";
            else
                sSQL += fromFieldValue;

            //======= 2. 运行SQL语句 ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;
            if (nRecordCount == 0)
                return "";

            //======= 3. 读取记录 ========
            row = dataSet.Tables[0].Rows[0];
            return row[toFieldName].ToString();
        }

        #endregion

    }
}
