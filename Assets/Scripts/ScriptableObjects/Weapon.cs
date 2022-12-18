using System;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Animations;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    public float damage;
    public int pelletsPerShot;
    public int maxBulletNumber;

    public float bulletSpread;
    public float range;

    [Header("Delays")]
    public float secondsBetweenShots;
    public float secondsBetweenReloads;

    [Header("Display")]
    public Sprite sprite;
    public Sprite shootSprite;
    public Sprite[] chamberSprites;

    public RuntimeAnimatorController AnimatorController
    {
        get
        {
            // Création de la courbe pour les animations
            var spriteBinding = new EditorCurveBinding();
            spriteBinding.type = typeof(SpriteRenderer);
            spriteBinding.path = "";
            spriteBinding.propertyName = "m_Sprite";

            var animatorController = new AnimatorController();
            // Ajout de la couche de base.
            animatorController.AddLayer("Base Layer");
            // Ajout trigger pour le tir
            animatorController.AddParameter("Shoot", AnimatorControllerParameterType.Trigger);

            // Ajout de l'état par défaut
            var idleState = animatorController.layers[0].stateMachine.AddState("Idle");
            idleState.motion = GenerateIdleAnimation(spriteBinding);

            // Ajout de l'état de tir
            var shootState = animatorController.layers[0].stateMachine.AddState("Shoot");
            shootState.motion = GenerateShootAnimation(spriteBinding);

            // Ajout de transition idle -> tir
            var shootTransition = idleState.AddTransition(shootState);
            shootTransition.AddCondition(AnimatorConditionMode.If, 0, "Shoot");
            shootTransition.hasExitTime = false;
            shootTransition.duration = 0.1f;

            // Ajout de transition tir -> idle
            var defaultTransition = shootState.AddTransition(idleState);
            defaultTransition.duration = 0.1f;
            defaultTransition.hasExitTime = true;
            defaultTransition.exitTime = 0.1f;

            return animatorController;
        }
    }

    /// <summary>
    /// Génération de l'animation de tir
    /// </summary>
    /// <param name="spriteBinding">Détermine la manière dont la courbe est liée à l'objet.<param>
    /// <returns>L'animation de tir</returns>
    private AnimationClip GenerateShootAnimation(EditorCurveBinding spriteBinding)
    {
        var shootAnimation = new AnimationClip();
        var spriteKeyFrames = new ObjectReferenceKeyframe[3];

        spriteKeyFrames[0] = new ObjectReferenceKeyframe();
        spriteKeyFrames[0].time = 0;
        spriteKeyFrames[0].value = sprite;
        spriteKeyFrames[1] = new ObjectReferenceKeyframe();
        spriteKeyFrames[1].time = 0.03f;
        spriteKeyFrames[1].value = shootSprite;
        spriteKeyFrames[2] = new ObjectReferenceKeyframe();
        spriteKeyFrames[2].time = 0.1f;
        spriteKeyFrames[2].value = sprite;

        AnimationUtility.SetObjectReferenceCurve(shootAnimation, spriteBinding, spriteKeyFrames);

        return shootAnimation;
    }

    /// <summary>
    /// Génération de l'animation par défaut
    /// </summary>
    /// <param name="spriteBinding">Détermine la manière dont la courbe est liée à l'objet.</param>
    /// <returns>L'animation par défaut</returns>
    private AnimationClip GenerateIdleAnimation(EditorCurveBinding spriteBinding)
    {
        var idleAnimation = new AnimationClip();
        var idleSpriteKeyFrames = new ObjectReferenceKeyframe[1];

        idleSpriteKeyFrames[0] = new ObjectReferenceKeyframe();
        idleSpriteKeyFrames[0].time = 0;
        idleSpriteKeyFrames[0].value = sprite;

        AnimationUtility.SetObjectReferenceCurve(idleAnimation, spriteBinding, idleSpriteKeyFrames);

        return idleAnimation;
    }
}
