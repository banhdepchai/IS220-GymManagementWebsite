using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using App.Models.Classes;
using App.Models.Memberships;
using App.Models.Products;

namespace App.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Category>().HasData(
            //    new Category
            //    {
            //        Id = 1,
            //        Title = "Phụ kiện thể thao",
            //        Description =
            //            "Dần dần mọi người đã ý thức được việc tập thể dục thể thao quan trọng thế nào trong cuộc sống hàng khi Covid đến, ai cũng mong muốn mình " +
            //            "có một thể trạng sức khỏe tốt để có thể đấu tranh với bệnh tật vây quanh ta mỗi ngày. Có rất nhiều phương pháp để tập luyện như tập Gym, " +
            //            "chạy bộ, bơi lội, đá bóng để có thể rèn luyện sức khỏe của bản thân. Nhưng đi kèm với đó, để có thể tập luyện một cách dễ dàng, hiệu quả " +
            //            "và tránh những rủi ro không đáng có trong tập luyện nhất thì chắc chắn bạn không thể thiếu những trợ thủ đắc lực của mình đó chính là những " +
            //            "phụ kiện thể thao, tập gym.  Tại thời điểm hiện tại, phụ kiện thể thao tập gym rất đa dạng, có nhiều mẫu mã và mỗi sản phẩm đều chứa một " +
            //            "công dụng và lợi ích khác nhau giúp hỗ trợ tập luyện hiệu quả. Cũng chính vì điều này, đặc biệt là với những người mới chơi thể thao đều " +
            //            "cân nhắc đắn đo khi lựa chọn các loại phụ kiện thể thao, tập gym phù hợp nhất với mình. Cho nên, Gymstore luôn cố gắng đưa cho bạn những " +
            //            "lựa chọn tốt nhất để có thể mua các sản phẩm phụ kiện thể thao, tập gym chính hãng, uy tín giá tốt nhất có thể. ",
            //        Slug = "phu-kien-the-thao"
            //    },
            //    new Category
            //    {
            //        Id = 2,
            //        Title = "Hỗ Trợ Giảm Mỡ",
            //        Description =
            //            "HỖ TRỢ ĐỐT MỠ là các sản phẩm có công thức mạnh mẽ trong việc tăng khả năng sinh nhiệt của cơ thể, hỗ trợ khả năng đốt cháy chất béo tự " +
            //            "nhiên. Với một số chất nổi bật như CLA, L-Carnitine, Yohimbine, Green Tea Extract ",
            //        Slug = "ho-tro-giam-mo"
            //    },
            //    new Category
            //    {
            //        Id = 3,
            //        Title = "Sức Khỏe Toàn Diện",
            //        Description = "Sức Khỏe Toàn Diện",
            //        Slug = "suc-khoe-toan-dien"
            //    },
            //    new Category
            //    {
            //        Id = 4,
            //        Title = "Sữa tăng cân",
            //        Description =
            //            "Sữa Tăng Cân Mass Gainer là dòng sữa tăng cân cho người gầy hỗ trợ bổ sung hàm lượng lớn Calories từ Protein, Carb, Fat, các Vitamin và " +
            //            "khoáng chất hỗ trợ cho người gầy tăng cân dễ dàng và hiệu quả. Sữa tăng cân mass gainer có ưu điểm phù hợp với nhiều đối tượng khác nhau, " +
            //            "sản phẩm chủ yếu hỗ trợ tăng cân cho người lớn, người tập Gym muốn tăng cân hiệu quả.",
            //        Slug = "sua-tang-can"
            //    }
            //);

            modelBuilder.Entity<Membership>().HasData(
                new Membership()
                {
                    MembershipId = 4,
                    Level = "Đồng",
                    Fee = 500000,
                    Duration = 3,
                    Hours = 2,
                    Bonus = "Ưu tiên xếp lớp"
                },
                new Membership()
                {
                    MembershipId = 5,
                    Level = "Bạc",
                    Fee = 1000000,
                    Duration = 3,
                    Hours = 3,
                    Bonus = "Được sử dụng phòng tắm, và cá tiện ích gói trên"
                },
                new Membership()
                {
                    MembershipId = 6,
                    Level = "Vàng",
                    Fee = 30000000,
                    Duration = 3,
                    Hours = 4,
                    Bonus = "Có huấn luyện viên cá nhân, và cá tiện ích gói trên"
                }
            );

            //modelBuilder.Entity<Room>().HasData(
            //    new Room { RoomId = 1, RoomName = "Phòng tập 1", Capacity = 10 }
            //);

            //modelBuilder.Entity<Discount>().HasData(
            //    new Discount { Id = 1, Code = "Q2QTLHJ401", Percent = 5 },
            //    new Discount { Id = 2, Code = "Z4VBROL202", Percent = 5 },
            //    new Discount { Id = 3, Code = "R0MMKGA003", Percent = 5 },
            //    new Discount { Id = 4, Code = "H6IFRNW004", Percent = 5 },
            //    new Discount { Id = 5, Code = "M5XHBVT605", Percent = 5 },
            //    new Discount { Id = 6, Code = "I4OPEJX206", Percent = 5 },
            //    new Discount { Id = 7, Code = "E5LLKGO207", Percent = 5 },
            //    new Discount { Id = 8, Code = "P4BMSJG608", Percent = 5 },
            //    new Discount { Id = 9, Code = "Z9BSUYY809", Percent = 5 },
            //    new Discount { Id = 10, Code = "S2KGNMD810", Percent = 5 },
            //    new Discount { Id = 11, Code = "Y4WUAMW811", Percent = 5 },
            //    new Discount { Id = 12, Code = "I1MWVIM912", Percent = 5 },
            //    new Discount { Id = 13, Code = "H3WWKLL813", Percent = 5 },
            //    new Discount { Id = 14, Code = "C7GFORG414", Percent = 5 },
            //    new Discount { Id = 15, Code = "K5KJVPS315", Percent = 5 },
            //    new Discount { Id = 16, Code = "U6BROHI716", Percent = 5 },
            //    new Discount { Id = 17, Code = "X4WFDAR217", Percent = 5 },
            //    new Discount { Id = 18, Code = "M3NIZFV318", Percent = 5 },
            //    new Discount { Id = 19, Code = "L4DGUVJ619", Percent = 5 },
            //    new Discount { Id = 20, Code = "X9XOJTR620", Percent = 5 },
            //    new Discount { Id = 21, Code = "F4CTPLZ421", Percent = 5 },
            //    new Discount { Id = 22, Code = "J6VHWGW422", Percent = 5 },
            //    new Discount { Id = 23, Code = "F6ZYLYU323", Percent = 5 },
            //    new Discount { Id = 24, Code = "O7WGRLR824", Percent = 5 },
            //    new Discount { Id = 25, Code = "A1XHZUD525", Percent = 5 },
            //    new Discount { Id = 26, Code = "L7VXFPZ226", Percent = 5 },
            //    new Discount { Id = 27, Code = "U5UBXJP727", Percent = 5 },
            //    new Discount { Id = 28, Code = "Q7RZSJF128", Percent = 5 },
            //    new Discount { Id = 29, Code = "E9YGYAS029", Percent = 5 },
            //    new Discount { Id = 30, Code = "M0NHSOR730", Percent = 5 },
            //    new Discount { Id = 31, Code = "Z5UWPAC931", Percent = 5 },
            //    new Discount { Id = 32, Code = "M2WNCKA432", Percent = 5 },
            //    new Discount { Id = 33, Code = "O7LGIUA533", Percent = 5 },
            //    new Discount { Id = 34, Code = "I3UHOBO634", Percent = 5 },
            //    new Discount { Id = 35, Code = "P1PKUFX435", Percent = 5 },
            //    new Discount { Id = 36, Code = "J9HRPEK936", Percent = 5 },
            //    new Discount { Id = 37, Code = "N2YVKJG837", Percent = 5 },
            //    new Discount { Id = 38, Code = "C9SJLPI138", Percent = 5 },
            //    new Discount { Id = 39, Code = "V6UCLLH439", Percent = 5 },
            //    new Discount { Id = 40, Code = "R3PEANZ640", Percent = 5 },
            //    new Discount { Id = 41, Code = "D4DHJYY741", Percent = 5 },
            //    new Discount { Id = 42, Code = "W7OFVKE042", Percent = 5 },
            //    new Discount { Id = 43, Code = "T7QMCWP843", Percent = 5 },
            //    new Discount { Id = 44, Code = "R6XCMVL144", Percent = 5 },
            //    new Discount { Id = 45, Code = "P7NLCSV945", Percent = 5 },
            //    new Discount { Id = 46, Code = "C6ZLXUW846", Percent = 5 },
            //    new Discount { Id = 47, Code = "Y4SHNFC447", Percent = 5 },
            //    new Discount { Id = 48, Code = "N9HOOXK148", Percent = 5 },
            //    new Discount { Id = 49, Code = "O4NBFXN149", Percent = 5 },
            //    new Discount { Id = 50, Code = "O5YYVEZ450", Percent = 5 },
            //    new Discount { Id = 51, Code = "K1HYHTL451", Percent = 10 },
            //    new Discount { Id = 52, Code = "O9IKYGO052", Percent = 10 },
            //    new Discount { Id = 53, Code = "J6KFNAP653", Percent = 10 },
            //    new Discount { Id = 54, Code = "H7TQVVM054", Percent = 10 },
            //    new Discount { Id = 55, Code = "X6IJXCK655", Percent = 10 },
            //    new Discount { Id = 56, Code = "O4MAHTL356", Percent = 10 },
            //    new Discount { Id = 56, Code = "S0BJAKQ757", Percent = 10 },
            //    new Discount { Id = 57, Code = "U1CWCOF258", Percent = 10 },
            //    new Discount { Id = 58, Code = "W1MFNUV359", Percent = 10 },
            //    new Discount { Id = 59, Code = "T1CRMIE960", Percent = 10 },
            //    new Discount { Id = 60, Code = "R3MHXHI061", Percent = 10 },
            //    new Discount { Id = 61, Code = "C1WRHPY362", Percent = 10 },
            //    new Discount { Id = 62, Code = "H3SQAKC463", Percent = 10 },
            //    new Discount { Id = 63, Code = "J4WZPUF964", Percent = 10 },
            //    new Discount { Id = 64, Code = "D7MJCTV065", Percent = 10 },
            //    new Discount { Id = 65, Code = "H1JWFNA766", Percent = 10 },
            //    new Discount { Id = 66, Code = "V1OFGMV967", Percent = 10 },
            //    new Discount { Id = 67, Code = "H4FDRFU768", Percent = 10 },
            //    new Discount { Id = 68, Code = "B2NVTYU469", Percent = 10 },
            //    new Discount { Id = 69, Code = "K9BTMDE770", Percent = 10 },
            //    new Discount { Id = 70, Code = "T9EAIIA471", Percent = 10 },
            //    new Discount { Id = 71, Code = "L5OZWIP472", Percent = 10 },
            //    new Discount { Id = 72, Code = "F7AVIYC073", Percent = 10 },
            //    new Discount { Id = 73, Code = "P1LYLYU374", Percent = 10 },
            //    new Discount { Id = 74, Code = "F1RJTIG275", Percent = 10 },
            //    new Discount { Id = 75, Code = "M0VSVEL376", Percent = 10 },
            //    new Discount { Id = 76, Code = "T2LPATP477", Percent = 10 },
            //    new Discount { Id = 77, Code = "O2TCIQP878", Percent = 10 },
            //    new Discount { Id = 78, Code = "V2OWXLA979", Percent = 10 },
            //    new Discount { Id = 79, Code = "S7LXPOT080", Percent = 10 },
            //    new Discount { Id = 80, Code = "Y1YEKDE481", Percent = 10 },
            //    new Discount { Id = 81, Code = "O1KANNG982", Percent = 10 },
            //    new Discount { Id = 82, Code = "U3YJGVE883", Percent = 10 },
            //    new Discount { Id = 83, Code = "K7FSTQB784", Percent = 10 },
            //    new Discount { Id = 84, Code = "Y0JJMZF485", Percent = 10 },
            //    new Discount { Id = 85, Code = "X2KLFRO886", Percent = 10 },
            //    new Discount { Id = 86, Code = "L5NIBJE287", Percent = 10 },
            //    new Discount { Id = 87, Code = "X8RSTXB888", Percent = 10 },
            //    new Discount { Id = 88, Code = "R2JTLWV889", Percent = 10 },
            //    new Discount { Id = 89, Code = "W5SURHV490", Percent = 20 },
            //    new Discount { Id = 90, Code = "B6AJJHA091", Percent = 20 },
            //    new Discount { Id = 91, Code = "S0ZFFTD292", Percent = 20 },
            //    new Discount { Id = 92, Code = "K7CAKCQ193", Percent = 20 },
            //    new Discount { Id = 93, Code = "X7CQJTD794", Percent = 20 },
            //    new Discount { Id = 94, Code = "F6SMWTI295", Percent = 20 },
            //    new Discount { Id = 95, Code = "X1TTIQQ796", Percent = 20 },
            //    new Discount { Id = 96, Code = "Y7MOOJD997", Percent = 20 },
            //    new Discount { Id = 97, Code = "D7YIYRO998", Percent = 20 },
            //    new Discount { Id = 98, Code = "Z5FUNZS799", Percent = 20 }
            //);
        }
    }
}