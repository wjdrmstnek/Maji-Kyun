// 근수 프레임워크 - 유용한 기능 모음
using System.Collections;
// 정적 클래스로 정의
public static class GeunSuUtility
{
    // Fisher-Yates Shuffle
    public static T[] Shuffle<T>(T[] array, int randomSeed)
    {
        // 랜덤 숫자를 시스템에서 받아와서 무작위로 설정한다
        System.Random rand = new System.Random(randomSeed);
        // 마지막 루프 생략
        for (int i = 0; i < array.Length - 1; i++)
        {
            // i번째 원소를 랜덤 원소와 교체한다
            int randIndex = rand.Next(i, array.Length);
            // 임시로 랜덤번째 값을 저장해둘 변수를 정의한다
            T temp = array[randIndex];
            // i번째 원소를 랜덤에 넣어 섞는다
            array[randIndex] = array[i];
            // 마지막으로 아까 저장해뒀던 값을 배열에 넣어 섞는다
            array[i] = temp;
        }
        // 셔플이 완료된 배열을 리턴한다
        return array;
    }
}