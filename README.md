# LobotomyVR
<img width="401" alt="LobotomyVR_Image" src="https://github.com/user-attachments/assets/3da88597-eba7-4638-9dcc-ee01b1dad83a" />
</br>
환상체를 관리하고 생존하세요!
마을을 지켜주세요!<br>
<br>
**Lovotomy VR** 프로젝트 문의 게임인 로보토미 코퍼레이션의 팬 게임으로 
기존 2D 탑다운 시뮬레이션과 달리
VR에서는 플레이어가 실제로 시설 내를 움직이고, 관리법을 직접 수행하며, 환상체의 탈출이나 직원의 사망 등 위기 상황을 몰입감 있게 경험할 수 있습니다.
환상체마다 고유 관리법이 존재하고, 이를 지키지 않으면 탈출 사태가 발생해 플레이어는 에너지 할당량을 채우는 동시에 직원 피해를 최소화해야 합니다.
---

## 주요 기능 (Key Features)

- **적 AI**: 확장성 높은 FSM을 사용하여 플레이어를 추격/공격하는 적 AI 구현 적의 상태(예: 무기 보유 여부)에 따라 다른 행동을 수행, 각 부위에 콜라이더 및 데미지 계수를 설정하여 명중 위치에 따라 피해량 차등 적용.
- **다양한 상호작용 아이템**: Pistol, Machine Gun, Grenade 등 다양한 적 공격 수단 구현, 힐팩, 탄창, 총알, 적 등 다양한 상호작용 요소
- **보스 AI**: 각 패턴은 개별 쿨타임을 가지며, LINQ를 활용해 쿨타임이 끝난 패턴만 선택적으로 발동하도록 구현.
- **특수 능력**: 사용 시 잔상 이펙트 (Mesh 베이킹), 화면 연출, Time.timeScale 조절로 슬로우 효과 연출
- **VR IK 기능**: VR 컨트롤러의 위치와 회전을 기반으로 캐릭터 양손이 자연스럽게 따라가도록 구현, 손에 **IK**를 적용하여 플레이어의 몰입감을 극대화함.
---

## 플레이 방법 (How to Play)

1. **게임 설치 및 실행**:
   - PC에 게임을 설치하고 실행합니다.

2. **메뉴 화면**:
   - 게임 시작 시 총을 집고 플레이 버튼을 사격합니다. 
3. **인 게임**:
   - 몰려오는 적을 막으며 최대한 높은 점수를 얻으세요!   
---

## 개발 환경 (Development Environment)

| 항목              | 내용                          |
|-------------------|------------------------------|
| **Engine**          | Unity 3D                     |
| **Language**          | C#                           |
| **Platform**        | PC, Mac                 |
| **Dependencies**     | Oculus          |

---

## IK 기능을 사용한 자유로운 VR 캐릭터 조종 및 다양한 상호작용 구현  

### 플레이어 이동, VR IK 적용
<img width="175" alt="Image" src="https://github.com/user-attachments/assets/96433834-cfeb-49b0-b91a-fa385a1acdcb" />
<br>
- VR 컨트롤러의 위치와 회전을 기반으로 캐릭터 양손이 자연스럽게 따라가도록 구현.
- 손에 **IK**를 적용하여 플레이어의 몰입감을 극대화함.
  
### 다양한 상호작용 오브젝트
<img width="359" alt="Image" src="https://github.com/user-attachments/assets/76918879-b2ba-407c-abe2-4439b9cbe119" />
<br>
- 인터페이스를 사용하여 효율적인 상호작용 기능을 구현했으며 장착 시 Grab Pose를 사용, 장착 중 사용 기능, 장착 해제 시 던지는 기능 등을 구현하였고 각 오브젝트 별로 다르도록 구현하였습니다.

### Ragdoll 적용 및 적 AI 구현
<img width="494" alt="Image" src="https://github.com/user-attachments/assets/f5610d3a-f7d4-49fd-a8f7-f5bcc5474491" /><br>
<img width="323" alt="Image" src="https://github.com/user-attachments/assets/546b4b32-c908-42cf-9d44-3053961d66e5" />
<br>
- 적의 공격, 이동 상태를 분리하기 위해 FSM을 사용했습니다 적의 상태(예: 무기 보유 여부)에 따라 다른 행동을 수행하도록 구현하였습니다. 
- 기존에 사용하던 enum 방식의 FSM이 아닌 클래스를 만들어 딕셔너리 형태로 저장하는 FSM을 사용하여 더욱 확장성을 높일 수 있었습니다.
- 적에게 래그돌을 적용하여 던지거나 휘두를 때 자연스럽게 관절이 날아감.
- 각 부위에 콜라이더 및 데미지 계수를 설정하여 명중 위치에 따라 피해량 차등 적용.
- 각 패턴은 개별 쿨타임을 가지며, LINQ를 활용해 쿨타임이 끝난 패턴만 선택적으로 발동하도록 구현.

---
### Notion 
- [https://www.notion.so/14b9dfc52f178026b95cf9ace5814eb0?pvs=74](https://buly.kr/BeKLB2M)
### 참고 영상
- https://buly.kr/DEZA0Ok
---

## 라이선스 (License)

이 프로젝트는 MIT 라이선스 하에 배포됩니다. 자세한 내용은 `LICENSE` 파일을 참조하세요.
