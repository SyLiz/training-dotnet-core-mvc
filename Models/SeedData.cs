using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcNewsFlash.Data;

namespace MvcNewsFlash.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MvcNewsFlashContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MvcNewsFlashContext>>()))
            {
                // Look for any movies.
                if (context.News.Any())
                {
                    return;   // DB has been seeded
                }

                context.News.AddRange(
                    new News
                    {
                        Title = "'ถ้ำหลวง' บทเรียนครั้งสำคัญของสื่อฯ ไทย",
                        ReleaseDate = DateTime.Parse("2018-7-12 13:00"),
                        Description = "องค์กรวิชาชีพสื่อมวลชนจำต้องสมควรที่ต้องหาทางป้องกันไม่ให้เกิดการละเมิดสิทธิมนุษยชน ไม่ให้มีการรายงานข่าวที่ดราม่า ไม่ให้มี Fake news หรือข่าวปลอมออกมาสร้างความสับสน",
                        ImagePath = "temp-49a23190f0f8c50a56d054ed4e855f44.jpeg"
                    },

                    new News
                    {
                        Title = "หวยออกวันที่ 17 ม.ค. นักคำนวณพาส่องสถิติ พบ เลขเด็ด 50 ออกซ้ำ 2 รอบ",
                        ReleaseDate = DateTime.Parse("2021-1-11 08:00"),
                        Description = "จากกรณี สำนักงานสลากกินแบ่งรัฐบาล เห็นสมควรให้มีการเปลี่ยนแปลงการออกสลากถาวร ในเดือนมกราคมของทุกปี เพื่อมิให้ตรงกับ วันครูแห่งชาติ",
                        ImagePath = "temp-49a23190f0f8c50a56d054ed4e855f44.jpeg"
                    },

                    new News
                    {
                        Title = "ศูนย์ กศน.อำเภออมก๋อย ยกเลิกประกาศ งดรับบริจาคของช่วยเด็ก",
                        ReleaseDate = DateTime.Parse("2021-1-11 17:16"),
                        Description = "ศูนย์ กศน.อำเภออมก๋อย ออกประกาศยกเลิกคำสั่งงดรับบริจาคของ หลังมีดราม่า พิมรี่พาย อ้างการสื่อสารคลาดเคลื่อน",
                        ImagePath = "temp-49a23190f0f8c50a56d054ed4e855f44.jpeg"
                    },

                    new News
                    {
                        Title = "คนฉกมือถือ พิพลอย จากที่เกิดเหตุ โอนเงินออกจากบัญชี ญาติให้โอกาสเอามาคืน",
                        ReleaseDate = DateTime.Parse("2021-1-11 19:54"),
                        Description = "ญาติเผย มีคนฉกโทรศัพท์มือถือ พิพลอย เน็ตไอดอลสาว ไปจากที่เกิดเหตุ และโอนเงินออกจากบัญชี ให้โอกาสเอามาคืน ก่อนวันเผา อังคารนี้",
                        ImagePath = "temp-49a23190f0f8c50a56d054ed4e855f44.jpeg"
                    },

                    new News
                    {
                        Title = "มนต์เสน่ห์เอฟเอคัพ",
                        ReleaseDate = DateTime.Parse("2021-1-12 05:54"),
                        Description = "ศึกฟุตบอลถ้วยเอฟเอ คัพ เรียกได้ว่าเป็นฟุตบอลถ้วยที่เก่าแก่ที่สุดในโลก และเป็นอีกหนึ่งฟุตบอลถ้วยที่มีเสน่ห์มากเพราะเปิดโอกาสให้กับทุกทีมที่สังกัดอยู่ในประเทศอังกฤษลงทำการแข่งขัน",
                        ImagePath = "temp-49a23190f0f8c50a56d054ed4e855f44.jpeg"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
