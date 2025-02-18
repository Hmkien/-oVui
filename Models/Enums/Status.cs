using System.ComponentModel;

namespace DoVuiHaiNao.Models.Enums;

public enum Status
{
    [Description("Đã duyệt")]
    Approved = 0,

    [Description("Chờ duyệt")]
    Pending = 1,

    [Description("Từ chối")]
    Rejected = 2,

    [Description("Xóa")]
    Cancelled = 3,

}
