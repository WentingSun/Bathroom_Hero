using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabMainItem : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    public BaseMusicItem realMop;
    public BaseMusicItem realPipe;
    public GameManager gameManager;  // 确保你有对GameManager的引用
    public GameObject fakeMop;
    public GameObject fakePipe;

    private Vector3 mopInitialPosition;
    private Vector3 pipeInitialPosition;
    private Quaternion mopInitialRotation;
    private Quaternion pipeInitialRotation;

    private static BaseMusicItem currentlySelectedRealItem; // 记录当前激活的真物品

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        // 记录假物品的初始位置和旋转
        mopInitialPosition = fakeMop.transform.position;
        pipeInitialPosition = fakePipe.transform.position;
        mopInitialRotation = fakeMop.transform.rotation;
        pipeInitialRotation = fakePipe.transform.rotation;

        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrab);
        }
        else
        {
            Debug.LogError("XRGrabInteractable component is missing!");
        }

        // 确保GameManager引用已设置
        if (gameManager == null)
        {
            gameManager = GameManager.Instance; // 获取GameManager的实例
        }
        // 确保GameManager引用已设置

        if (gameManager == null)
        {
            Debug.LogError("GameManager instance is not assigned or not found.");
        }
        }

    private void OnGrab(SelectEnterEventArgs args)
    {
        // 处理假Mop或假Pipe的抓取
        if (gameObject == fakeMop)
        {
            HandleFakeItem(fakeMop, realMop, mopInitialPosition, mopInitialRotation, realPipe, fakePipe);
            // 更新玩家状态为选择了Mop
            gameManager.UpdatePlayerState(PlayerState.playerSelectMop);
        }
        else if (gameObject == fakePipe)
        {
            HandleFakeItem(fakePipe, realPipe, pipeInitialPosition, pipeInitialRotation, realMop, fakeMop);
            // 更新玩家状态为选择了Pipe
            gameManager.UpdatePlayerState(PlayerState.playerSelectTubelight);
        }
    }

    private void HandleFakeItem(GameObject fakeItem, BaseMusicItem newRealItem, Vector3 initialPosition, Quaternion initialRotation, BaseMusicItem currentRealItem, GameObject currentFakeItem)
    {
        // 取消当前激活的真物品
        if (currentlySelectedRealItem != null && currentlySelectedRealItem != newRealItem)
        {
            currentlySelectedRealItem.UnSelected(); // 调用UnSelected方法
            ResetFakeItem(currentFakeItem); // 重置当前假物品的位置和材质
        }

        // 激活新的真物品
        fakeItem.GetComponent<Renderer>().enabled = false; // 隐藏假物品
        fakeItem.transform.position = initialPosition; // 重置假物品位置
        fakeItem.transform.rotation = initialRotation; // 重置假物品旋转

        newRealItem.BeSelected(); // 调用BeSelected方法
        currentlySelectedRealItem = newRealItem; // 更新当前激活的真物品
    }

    private void ResetFakeItem(GameObject fakeItem)
    {
        if (fakeItem != null)
        {
            fakeItem.GetComponent<Renderer>().enabled = true; // 显示假物品
            if (fakeItem == fakeMop)
            {
                fakeItem.transform.position = mopInitialPosition;
                fakeItem.transform.rotation = mopInitialRotation; // 恢复初始旋转
            }
            else if (fakeItem == fakePipe)
            {
                fakeItem.transform.position = pipeInitialPosition;
                fakeItem.transform.rotation = pipeInitialRotation; // 恢复初始旋转
            }
        }
    }
}
