﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StringTest
{
    class Program
    {
        static void Main(string[] args)
        {
            {//字符串取值测试
                StringAndChar.ToUp();
            }

            {//字符串格式化json
                JsonFormat.StringToJson();
            }

            {//正则测试
                string text = @"华安睿享定期开放混合型发起式证券投资基金更新的招募说明书摘要2018年第2号基金管理人：华安基金管理有限公司基金托管人：中国民生银行股份有限公司二○一八年十二月		重要提示		华安睿享定期开放混合型发起式证券投资基金（以下简称“本基金”）由华安基金管理有限公司（以下简称“基金管理人”）依照有关法律法规及约定发起，并经中国证券监督管理委员会2016年6月28日证监许可[2016]1445号文准予注册。本基金的基金合同和招募说明书已通过《中国证券报》、《上海证券报》、《证券时报》和基金管理人的互联网网站（www.huaan.com.cn）进行了公开披露。本基金的基金合同自2016年11月2日正式生效。基金管理人保证招募说明书的内容真实、准确、完整。本招募说明书经中国证监会注册，但中国证监会对本基金募集的注册，并不表明其对本基金的投资价值和市场前景作出实质性判断或保证，也不表明投资于本基金没有风险。证券投资基金（以下简称“基金”）是一种长期投资工具，其主要功能是分散投资，降低投资单一证券所带来的个别风险。基金不同于银行储蓄和债券等能够提供固定收益预期的金融工具，投资者购买基金，既可能按其持有份额分享基金投资所产生的收益，也可能承担基金投资所带来的损失。本基金在投资运作过程中可能面临各种风险，包括但不限于市场风险、管理风险、流动性风险、技术风险、道德风险、合规风险、不可抗力风险以及定期开放运作方式下特有的申购赎回相关风险和强制关闭风险等。本基金可投资于中小企业私募债券，其与一般信用债券相比，存在更大的信用风险和流动性风险。更大的信用风险在于该类债券发行主体的资产规模较小、经营的波动性较大，信息披露不透明，也大大提高了分析并跟踪发债主体信用基本面的难度；更大的流动性风险在于该类债券采取非公开方式发行和交易，外部评级机构一般不对这类债券进行外部评级，可能会降低市场对该类债券的认可度，从而影响该类债券的市场流动性。这些特点使得中小企业私募债券可能出现因信用水平波动而引起的价格较大幅度波动，从而影响基金总体投资收益；同时，流动性差导致的变现困难，也会给基金总体投资组合带来流动性冲击。本基金可投资资产支持证券，资产支持证券存在一定的信用风险、利率风险、流动性风险、提前偿付风险、操作风险和法律风险。本基金为混合型基金，其预期收益及预期风险水平高于债券型基金和货币市场基金，但低于股票型基金，属于中等预期收益风险水平的投资品种。基金的过往业绩并不预示其未来表现。投资者应当认真阅读基金合同、招募说明书等基金法律文件，了解基金的风险收益特征，并根据自身的投资目的、投资期限、投资经验、资产状况等判断基金是否和投资者的风险承受能力相适应，并自主做出投资决策，自行承担投资风险。基金管理人依照恪尽职守、诚实信用、谨慎勤勉的原则管理和运用基金资产，但不保证本基金一定盈利，也不保证最低收益。本基金的过往业绩及其净值高低并不预示其未来业绩表现，基金管理人管理的其他基金的业绩亦不构成对本基金业绩表现的保证。基金管理人提醒投资者基金投资的“买者自负”原则，在作出投资决策后，基金运营状况与基金净值变化引致的投资风险，由投资者自行负担。投资者应当通过基金管理人或具有基金销售业务资格的其他机构认/申购和赎回基金，基金销售机构名单详见本招募说明书以及相关公告";
                text = Regular.GetTitle(text);
                Regular re = new Regular();
                text = "haaanjij2018年20182014第2号2018";
                text = re.RemoveNum(text);
            }

            {//日期测试
                DateOpreate d= new DateOpreate();
                Console.WriteLine("今天是"+d.GetWeek());
                Console.ReadKey();
            }

        }
    }
}
