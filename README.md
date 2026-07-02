# Quiz Game

Quiz Game là game hỏi đáp kết hợp chiến đấu theo lượt được phát triển bằng **Unity 6** và **C#**. Người chơi trả lời các câu hỏi để tấn công kẻ địch, trả lời sai sẽ bị phản công. Mục tiêu là đạt điểm số cao nhất trong từng chủ đề.

---

# 📌 Công nghệ sử dụng

- Unity 6
- C#
- Unity UI
- TextMeshPro
- JSON
- PlayerPrefs

---

# 🎯 Tính năng hiện có

## Gameplay
- Hệ thống chiến đấu theo lượt.
- Trả lời đúng để tấn công Enemy.
- Trả lời sai hoặc hết thời gian sẽ bị Enemy tấn công.
- Mỗi câu hỏi có thời gian đếm ngược.
- Enemy bị hạ sẽ hồi đầy máu (hoặc sinh Enemy mới).
- Player được hồi một phần HP sau mỗi lần hạ Enemy.
- Trò chơi tiếp tục cho đến khi Player bị hạ.

---

## Hệ thống câu hỏi

- Đọc dữ liệu từ file `questions.json`.
- Chia câu hỏi theo nhiều chủ đề.
- Chọn chủ đề trước khi bắt đầu trận đấu.
- Random câu hỏi trong từng chủ đề.
- Shuffle vị trí đáp án mỗi câu.
- Hiển thị màu đáp án đúng/sai sau khi chọn.

---

## Hệ thống điểm

- +10 điểm cho mỗi câu trả lời đúng.
- Hiển thị điểm trực tiếp trong trận đấu.
- Lưu High Score riêng cho từng chủ đề.

Ví dụ:

- Toán học
- Lịch sử
- Địa lý
- Công nghệ
- Âm nhạc
- ...

---

## UI

- Main Menu
- Chọn chủ đề
- Battle UI
- HP Bar
- Bộ đếm thời gian
- Tiến trình câu hỏi
- Hiển thị điểm
- Game Over Panel
- Retry
- Back To Menu

---

# 📂 Cấu trúc Project

```
Assets
│
├── Audio
├── Materials
├── Prefabs
├── Resources
│     questions.json
│
├── Scenes
│     MainMenuScene
│     CategoryScene
│     BattleScene
│
└── Scripts
      ├── Characters
      ├── Core
      ├── Data
      ├── Systems
      └── UI
```

---

# 🏗 Kiến trúc

Project được xây dựng theo hướng tách chức năng rõ ràng:

- Singleton
- Event
- Manager Pattern
- Battle State Machine
- Data Driven (JSON)

Các Manager hiện có:

- GameManager
- BattleSystem
- QuestionManager

---

# 🎮 Luồng chơi

Main Menu

↓

Chọn chủ đề

↓

Battle

↓

Trả lời câu hỏi

↓

Đúng → Player tấn công

Sai/Hết giờ → Enemy tấn công

↓

Enemy chết

↓

Enemy hồi đầy máu

↓

Player hồi một phần HP

↓

Tiếp tục trả lời câu hỏi

↓

Player chết

↓

Game Over

↓

Lưu High Score

---

# 🚧 Đang phát triển

- Audio Manager
- Nhạc nền
- Hiệu ứng âm thanh
- Pause Menu
- Settings
- Animation chiến đấu
- Hiệu ứng UI




---

# 👨‍💻 Tác giả

**Đạt Bùi**

Sinh viên định hướng Unity Game Developer.
