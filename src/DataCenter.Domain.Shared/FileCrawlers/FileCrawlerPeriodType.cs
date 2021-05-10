using System;
using System.Collections.Generic;
using System.Text;

namespace DataCenter.FileCrawlers
{
    /// <summary>
    /// 文件爬虫周期类型
    /// </summary>
    public enum FileCrawlerPeriodType
    {
        /// <summary>
        /// 秒
        /// </summary>
        Second,
        /// <summary>
        /// 分
        /// </summary>
        Minute,
        /// <summary>
        /// 时
        /// </summary>
        Hour,
        /// <summary>
        /// 天
        /// </summary>
        Day,
        /// <summary>
        /// 周
        /// </summary>
        Week,
        /// <summary>
        /// 月
        /// </summary>
        Month,
        /// <summary>
        /// 季
        /// </summary>
        Quarter,
        /// <summary>
        /// 年
        /// </summary>
        Year
    }
}
