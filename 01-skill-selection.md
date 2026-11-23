# 기업별 기술팩 가중치와 콜라보 후보

기업별 기술 데이터는 JSON에서 읽습니다. 기술팩을 뽑을 때 기업별로 보유한 기술 수를 반영하고, 모든 기업에 기본 가중치 1을 부여합니다. 관련 기업의 보유 조건이 충족되면 콜라보 기술 목록에서 해당 후보를 찾습니다.

## SkillBase.cs · 기술 보유 수 갱신

```csharp
protected int _skillNum;
public int skillNum { get { return _skillNum; } }
public virtual void SkillNumUp(SkillData skilldata, int num)
{
    _skillNum+= num;
}
```

## SkillManager.cs · 기업별 가중치

```csharp
int skillOneNum = _skill_One.skillNum + 1;
int skillTwoNum = _skill_Two.skillNum + 1;
int skillThreeNum = _skill_Three.skillNum + 1;
int skillFourNum = _skill_Four.skillNum + 1;
int skillFiveNum = _skill_Five.skillNum + 1;
```

## SkillManager.cs · 콜라보 후보 검색

```csharp
int index = _skill_Collab.skillList.FindIndex(skillData => skillData.skillIdx == Constants.INDEX_ONE_TWO);
```

세 발췌는 조성호 계정이 작성한 같은 커밋의 변경분에서 가져왔습니다. 실제 플레이에서는 원본의 기업별 스킬 데이터와 선택 UI에 연결됩니다. [작성 커밋](https://github.com/TeamNemesis/ProjectNemesis/commit/90fad4b350a9fa91739619b37dc6d7c9617e7d3d)
