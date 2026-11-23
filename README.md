# Project Nemesis · 조성호 포트폴리오

Unity 6 · C# · 4인 팀 프로젝트 · PC/Mobile

사이버펑크 액션 로그라이크입니다. 매 회차의 전투와 방 선택을 거치며 기술을 조합하고 보스에 도전합니다.

[시연 영상](https://youtu.be/AbBBSkmhfGs) · [Unity 스킬 데모](UnityDemo) · [스킬 코드 샘플](01-skill-selection.md) · [전체 클래스가 있는 포트폴리오 브랜치](https://github.com/whtjdgh1931/nemesis/tree/portfolio-main)

## 조성호 담당 구현

| 담당 기능 | 구현 내용 |
| --- | --- |
| 스킬 시스템 | JSON 기술 데이터, 기업별 기술팩 가중치, 콜라보 출현 조건, 기술 선택 UI 연동 |
| 플레이어 스탯 | 저장된 스탯으로 캐릭터 초기화, 공격 유형별 기본 피해 계산 |
| 서버 연동 | Firebase 로그인과 자동 로그인, 클리어 기록과 강화 상태 저장 |

### 대표 구현 · 기업별 기술팩과 콜라보

기업별 보유 기술 수에 따라 다음 기술팩의 가중치가 달라집니다. 관련 기업의 보유 조건이 충족되면 콜라보 후보를 선택지에 올립니다. 기술 데이터와 선택 규칙을 분리해 기술을 추가하거나 출현 조건을 조정할 수 있게 구성했습니다.

| 읽을 코드 | 확인할 내용 |
| --- | --- |
| [스킬 선택 코드 샘플](01-skill-selection.md) | 조성호 계정의 작성 커밋에서 발췌한 기업별 가중치 계산과 콜라보 후보 조회 |
| [SkillManager.cs](https://github.com/whtjdgh1931/nemesis/blob/portfolio-main/Assets/02_Scripts/00_Manager/SkillManager.cs) | 기술팩 선택과 콜라보 조건 검사 |
| [PlayerStatManager.cs](https://github.com/whtjdgh1931/nemesis/blob/portfolio-main/Assets/02_Scripts/00_Manager/PlayerStatManager.cs) | 스탯 처리와 전투 적용 |

## Unity 데모 실행

1. 저장소를 내려받은 뒤 Unity Hub에서 `UnityDemo` 폴더를 추가합니다.
2. Unity **6000.4.8f1**에서 `Assets/Scenes/PortfolioDemo.unity`를 열고 **Play**를 누릅니다.
3. 제시된 카드 중 하나를 선택해 기업별 보유 수와 가중치 변화를 확인합니다. 각 기업의 기술을 세 개씩 모으면 **Claim fusion upgrade**가 활성화됩니다.

데모 화면은 Unity 기본 GUI로 구성했습니다. 외부 에셋 구매나 서비스 계정 없이 실행할 수 있습니다.

데모는 기술 선택과 콜라보 조건을 체험할 수 있도록 별도로 구현했습니다. 시연 영상에서는 원작의 전투 흐름을 볼 수 있고, 포트폴리오 브랜치에는 전체 클래스가 있습니다.
