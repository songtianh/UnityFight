namespace gedou
{
    public static class AniName
    {
        public  const  string idle = "stand";
        public const string jump = "jump";
        public const string  move = "forward";
        public const string  attack0 = "attack1";
        public const string  attack1 = "attack2";
        public const string  attack2 = "attack3";
        public const string  attackback0 = "attack1back";
        public const string attackback1 = "attack2back";
        public const string attackback2 = "attack3back";
        public const string  defense = "defense";
        public const string  defenseback = "defenseback";
        public const string  hit = "hit1";
        public const string  hit2 = "hit2_1";
        public const string  hit3 = "hit2_2";
        public const string   hit4 = "hit2_3";
        public const string   hit5 = "hit3";
        public const string   hit2back = "hit2back";
        public const string   jumpattack = "jump_attack";
        public const string  skill1 = "skill1_1";
        public const string   skill2 = "skill1_2";
        public const string   skill3 = "skill2";
        public const string  xuli1 = "attack_xuli1";
        public const string  xuli2 = "attack_xuli2";
        public const string  collision = "collision";
        public const string  dead = "dead";
    }

    public static class EffName
    {
        public const string  atkHit = "attack_hit";
        public const string  defense = "defense";
        public const string  skill1_1hit = "skill1_1hit";
        public const string  skill1_2hit = "skill1_2hit";
        public const string  skill2 = "skill2";
        public const string  skill2_hit = "skill2_hit";
    }

    public enum StateEventType
    {
        keyDown,
        keyUp,
        hit,
        hitJump,
    }

    public enum ControlType
    {
        Touch,
        keyboard,
        Ai
    }

    public enum HeroType
    {
        Gedou,
        Cike
    }
    
    public enum MonsterType
    {
        gbl,
    }
    public enum CusEventType
    {
        ShowEff,
        SHOW_TXT,
        Shoot,
        ShowBulletEff
    }
}