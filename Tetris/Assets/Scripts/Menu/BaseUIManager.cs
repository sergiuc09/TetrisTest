using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public abstract class BaseUIManager : MonoBehaviour
{
    private Stack<Menu> _menuStack = new Stack<Menu>();
    private List<Menu> _menus = new List<Menu>();

    //protected bool HasSubMenus => _menuStack.Count > 0;

    private void Update()
    {
        if (PlayerInput.BackPressed())
            OnBackPressed();
    }

    #region Public

    public virtual void Open<T>() where T : Menu
    {
        Menu menu;
        int index = CheckMenuExists<T>();

        if (index >= 0)
        {
            menu = _menus[index];
            if (!menu.gameObject.activeSelf)
                menu.gameObject.SetActive(true);

            if (_menuStack.Contains(menu))
                return;
        }
        else menu = CreateInstance<T>();

        if (_menuStack.Count > 0 && menu.DisableMenusUnderneath)
        {
            foreach (var item in _menuStack)
            {
                item.gameObject.SetActive(false);

                // if menu DisableMenusUnderneath, that means it was disabled
                if (item.DisableMenusUnderneath) break;
            }
        }

        _menuStack.Push(menu);
    }

    public virtual void Close(Menu menu)
    {
        if (_menuStack.Count == 0)
        {
            Debug.LogErrorFormat(menu, "{0} cannot be closed because menu stack is empty", menu.GetType());
            return;
        }

        if (_menuStack.Peek() != menu)
        {
            Debug.LogErrorFormat(menu, "{0} cannot be closed because it is not on top of stack", menu.GetType());
            print(_menuStack.Peek().GetType());
            return;
        }

        _menuStack.Pop();

        if (menu.DestroyWhenClosed)
        {
            if (_menus.Contains(menu))
                _menus.Remove(menu);

            Destroy(menu.gameObject);
        }
        else menu.gameObject.SetActive(false);

        if (_menuStack.Count > 0)
        {
            foreach (var item in _menuStack)
            {
                item.gameObject.SetActive(true);

                if (item.DisableMenusUnderneath) break;
            }
        }
    }

    #endregion

    #region Private

    private int CheckMenuExists<T>() where T : Menu
    {
        int index = -1;

        for (int i = 0; i < _menus.Count; i++)
        {
            if (_menus[i] is T)
            {
                index = i;
                break;
            }
        }

        return index;
    }

    private T CreateInstance<T>() where T : Menu
    {
        Menu menu = Instantiate(GetPrefab<T>());
        menu.Initialization(this);
        _menus.Add(menu);

        return (T)menu;
    }

    private T GetPrefab<T>() where T : Menu
    {
        // Get prefab dynamically, based on public fields set from Unity
        // You can use private fields with SerializeField attribute too
        var fields = GetType().GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        foreach (var field in fields)
        {
            var prefab = field.GetValue(this) as T;
            if (prefab != null)
            {
                return prefab;
            }
        }

        throw new MissingReferenceException("Prefab not found for type " + typeof(T));
    }

    //
    private void OnBackPressed()
    {
        if (_menuStack.Count > 0)
            _menuStack.Peek().OnBackPressed();
        /*if (_menuStack.Count > 0)
            OnMenuClose(false);

        if (_menuStack.Count == 0)
            Application.Quit();*/
    }
    //

    #endregion

    protected void OnMenuClose(bool closee_all_menus)
    {
        if (closee_all_menus)
        {
            while (_menuStack.Count > 0)
            {
                Close(_menuStack.Peek());
            }
        }
        else Close(_menuStack.Peek());
    }
}
