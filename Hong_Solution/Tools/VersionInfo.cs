using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hong_Solution.Tools
{
    internal class VersionInfo
    {

        public DateTime Get_BuildDateTime(Version version = null)
        {
            // 주.부.빌드.수정
            // 주 버전    Major Number
            // 부 버전    Minor Number
            // 빌드 번호  Build Number
            // 수정 버전  Revision NUmber

            //매개 변수가 존재할 경우
            if (version == null)
                version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

            //세번째 값(Build Number)은 2000년 1월 1일부터
            //Build된 날짜까지의 총 일(Days) 수 이다.
            int day = version.Build;
            System.DateTime dtBuild = (new System.DateTime(2000, 1, 1)).AddDays(day);

            //네번째 값(Revision NUmber)은 자정으로부터 Build된
            //시간까지의 지나간 초(Second) 값 이다.
            int intSeconds = version.Revision;
            intSeconds = intSeconds * 2;
            dtBuild = dtBuild.AddSeconds(intSeconds);


            //시차 보정
            System.Globalization.DaylightTime daylingTime = TimeZone.CurrentTimeZone
                    .GetDaylightChanges(dtBuild.Year);
            if (System.TimeZone.IsDaylightSavingTime(dtBuild, daylingTime))
                dtBuild = dtBuild.Add(daylingTime.Delta);

            return dtBuild;
        }
    }
}
