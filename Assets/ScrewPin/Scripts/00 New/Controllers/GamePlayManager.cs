using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class GamePlayManager : GameMonobehavior
{
    public Action OnAddHole;
    public Action OnShuffle;
    public Action OnAutoMatch;

    public float coolDownTime = 3f; // Thời gian cooldown cho skill
    public int maxSkillUsage = 2; // Số lần tối đa skill có thể được sử dụng 
    public Skill[] skills;


    public static GamePlayManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            DestroyImmediate(gameObject);
        
    }
    
    private void Update()
    {
        foreach (Skill skill in skills)
        {
            if (Time.time > skill.nextSkillTime && skill.skillUsageCount < maxSkillUsage)
            {
                skill.skillButton.interactable = true; // Kích hoạt lại nút skill
                skill.coolDownImage.fillAmount = 0; // Ẩn hình ảnh cooldown
            }
            else if (skill.skillUsageCount >= maxSkillUsage)
            {
                skill.skillButton.interactable = false; // Vô hiệu hóa nút skill khi vượt quá số lần sử dụng
                skill.coolDownImage.fillAmount = 1; // Hiển thị hình ảnh cooldown đầy
            }
            else
            {
                skill.skillButton.interactable = false; // Vô hiệu hóa nút skill
                skill.coolDownImage.fillAmount = (skill.nextSkillTime - Time.time) / coolDownTime; // Cập nhật hình ảnh cooldown
            }
        }
    }

    public void UseSkill(string skillName)
    {
        
        Skill skill = System.Array.Find(skills, s => s.skillName == skillName);
        if (skill != null && Time.time > skill.nextSkillTime && skill.skillUsageCount < maxSkillUsage)
        {
            // Thực hiện skill của bạn ở đây
            Debug.Log(skillName + " skill used!");

            skill.skillUsageCount++; // Tăng số lần sử dụng skill
            skill.nextSkillTime = Time.time + coolDownTime; // Thiết lập thời gian cooldown

            if (skill.skillUsageCount >= maxSkillUsage)
            {
                skill.skillButton.transform.DOMoveY(-5f, 0.5f).OnComplete(() =>
                {
                    skill.skillButton.gameObject.SetActive(false);

                });
                skill.coolDownImage.fillAmount = 1; // Hiển thị hình ảnh cooldown đầy
            }
        }
    }

    public void ResetSkills()
    {
        foreach (Skill skill in skills)
        {
            skill.skillUsageCount = 0; // Đặt lại số lần sử dụng skill
            skill.nextSkillTime = 0f; // Đặt lại thời gian cooldown
            skill.skillButton.interactable = true; // Kích hoạt lại nút skill
            skill.coolDownImage.fillAmount = 0; // Đặt lại hình ảnh cooldown
        }
    }

    [System.Serializable]
    public class Skill
    {
        public string skillName; // Tên của skill
        public Button skillButton; // Nút để kích hoạt skill
        public Image coolDownImage; // Hình ảnh hiển thị cooldown
        [HideInInspector] public float nextSkillTime = 0f; // Thời gian mà skill có thể được sử dụng lại
        [HideInInspector] public int skillUsageCount = 0; // Đếm số lần skill được sử dụng
    }
}
