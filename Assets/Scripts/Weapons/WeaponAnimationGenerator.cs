#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

[CustomEditor(typeof(Weapon))]
[CanEditMultipleObjects]
[ExecuteInEditMode]
public class WeaponAnimationGenerator : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Generate Animator Controller"))
        {
            var weapon = target as Weapon;
            var path = GenerateAnimatorController(weapon.sprite, weapon.shootSprite);
            weapon.animatorControllerPath = path;
        }
    }

    private string SaveAnimatorController(AnimatorController animatorController)
    {
        var path = EditorUtility.SaveFilePanelInProject("Save Animator Controller", "New Animator Controller", "controller", "Save Animator Controller", "Assets/Animations");
        if (path.Length != 0)
        {
            AssetDatabase.CreateAsset(animatorController, path);
        }
        return path;
    }

    private void SaveAnimation(AnimationClip idleAnimation, AnimationClip shootAnimation)
    {
        var path = EditorUtility.SaveFilePanelInProject("Save idle Animation", "Idle", "anim", "Save Animation", "Assets/Animations");
        if (path.Length != 0)
        {
            AssetDatabase.CreateAsset(idleAnimation, path);
        }
        var path2 = EditorUtility.SaveFilePanelInProject("Save shoot Animation", "Shoot", "anim", "Save Animation", "Assets/Animations");
        if (path2.Length != 0)
        {
            AssetDatabase.CreateAsset(shootAnimation, path2);
        }
    }

    private string GenerateAnimatorController(Sprite sprite, Sprite shootSprite)
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
        idleState.motion = GenerateIdleAnimation(spriteBinding, sprite);

        // Ajout de l'état de tir
        var shootState = animatorController.layers[0].stateMachine.AddState("Shoot");
        shootState.motion = GenerateShootAnimation(spriteBinding, sprite, shootSprite);

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

        SaveAnimation(idleState.motion as AnimationClip, shootState.motion as AnimationClip);
        return SaveAnimatorController(animatorController);
    }

    /// <summary>
    /// Génération de l'animation de tir
    /// </summary>
    /// <param name="spriteBinding">Détermine la manière dont la courbe est liée à l'objet.<param>
    /// <returns>L'animation de tir</returns>
    private AnimationClip GenerateShootAnimation(EditorCurveBinding spriteBinding, Sprite sprite, Sprite shootSprite)
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
    private AnimationClip GenerateIdleAnimation(EditorCurveBinding spriteBinding, Sprite sprite)
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
#endif